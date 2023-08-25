using System;
using System.IO.Ports;
using System.Text;
using System.Threading;

namespace ChainEdge.Drivers;

public class ESCPOSSerialPrintDriver : Driver
{
    readonly SemaphoreSlim semaph = new(1);

    readonly SerialPort port = new()
    {
        BaudRate = 19200,
        Parity = Parity.None,
        DataBits = 8,
        StopBits = StopBits.One,
        ReadTimeout = 200,
        WriteTimeout = 200,
    };


    const string ESC = "\u001B";
    const string GS = "\u001D";
    const string FS = "\u001C";

    const string InitializePrinter = ESC + "@";
    const string BoldOn = ESC + "E" + "\u0001";
    const string BoldOff = ESC + "E" + "\0";
    const string DoubleOn = GS + "!" + "\u0011";


    const string ZhOn = FS + "&";


    public override void Reset()
    {
        var names = SerialPort.GetPortNames();
        foreach (var name in names)
        {
            semaph.Wait();
            try
            {
                port.PortName = name;
                port.Open();

                // make a retrieval
                if (TryGetStatus(out var v, period))
                {
                    port.Write(InitializePrinter);
                    port.WriteLine("Here is some normal text.");
                    port.WriteLine(BoldOn + "Here is some bold text." + BoldOff);
                    port.Write(ZhOn);
                    var b = GetGBKEncode("大字体测试");
                    port.Write(b, 0, b.Length);

                    port.WriteLine(ZhOn + unicodetogb("大字体测试"));
                    return; // keep COM port
                }

                // continue
                port.Close();
            }
            catch (UnauthorizedAccessException e) // used by other process
            {
            }
            catch (InvalidOperationException e) // port is open
            {
            }
            catch (Exception e)
            {
                if (port.IsOpen)
                {
                    port.Close();
                }
            }
            finally
            {
                semaph.Release();
            }
        }
    }

    public override string Label => "票据打印";

    const string DoubleOff = GS + "!" + "\0";

    static readonly byte[] DLE_EOT_1 = { 0x10, 0x04, 1 };

    static readonly byte[] buf = new byte[1024];

    public bool TryGetStatus(out byte result, int milliseconds)
    {
        result = default;

        semaph.Wait(milliseconds);
        try
        {
            Weigh:

            Array.Clear(buf);

            port.Write(DLE_EOT_1, 0, DLE_EOT_1.Length);

            Thread.Sleep(period);

            var num = port.Read(buf, 0, 1);
            if (num != 1)
            {
                status = STU_ERR;
                return false;
            }

            if ((buf[0] & 0x12) != 0x12)
            {
                status = STU_ERR;
                return false;
            }

            result = buf[0];

            status = STU_READY;
            return true;
        }
        catch (Exception e)
        {
            status = STU_ERR;
            return false;
        }
        finally
        {
            semaph.Release();
        }
    }

    private static readonly byte[]
        FS_A = { 0x1c, 0x26 }, // FS &
        FSS = { 0x1c, 0x21, 0 };

    public ESCPOSSerialPrintDriver SetFS_A()
    {
        port.Write(FS_A, 0, FS_A.Length);

        return this;
    }


    public ESCPOSSerialPrintDriver TT()
    {
        port.Write(FSS, 0, FSS.Length);

        return this;
    }


    public ESCPOSSerialPrintDriver HT()
    {
        port.Write("\t");

        return this;
    }

    public ESCPOSSerialPrintDriver T(string v)
    {
        port.Write(v);

        return this;
    }

    public ESCPOSSerialPrintDriver T(decimal v)
    {
        port.Write(v.ToString());

        return this;
    }

    public static byte[] GetGBKEncode(string unicodeString)
    {
        Encoding unicode = Encoding.Unicode;

        Encoding gbk = Encoding.GetEncoding(936);

        byte[] unicodeBytes = unicode.GetBytes(unicodeString);

        byte[] gbkBytes = Encoding.Convert(unicode, gbk, unicodeBytes);

        return gbkBytes;

        // int i = 0;
        // string result = "";
        //
        // while (i < gbkBytes.Length)
        // {
        //
        //     if (gbkBytes[i] <= 127)
        //     {
        //         result += (char)gbkBytes[i];
        //
        //     }
        //     else
        //     {
        //         result += "%" + gbkBytes[i].ToString("X");
        //
        //     }
        //     i++;
        // }
        // return result;
    }
    
    public string unicodetogb(string text)
    {
        System.Text.RegularExpressions.MatchCollection mc = System.Text.RegularExpressions.Regex.Matches(text, "\\\\u([\\w]{4})");
        if (mc != null && mc.Count > 0)
        {
            foreach (System.Text.RegularExpressions.Match m2 in mc)
            {
                string v = m2.Value;
                string word = v.Substring(2);
                byte[] codes = new byte[2];
                int code = Convert.ToInt32(word.Substring(0, 2), 16);
                int code2 = Convert.ToInt32(word.Substring(2), 16);
                codes[0] = (byte)code2;
                codes[1] = (byte)code;
                text = text.Replace(v, Encoding.Unicode.GetString(codes));
            }
        }
        else
        {

        }
        return text;
    }
}