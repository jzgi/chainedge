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
    const string ZhOff = FS + ".";
    const string ZhCharset = FS + "!" + "\u0001";


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
                    // port.WriteLine("As a result");
                    // var b = ToGbk("这是一个核心");
                    // port.Write(b, 0, b.Length);
                    // port.WriteLine("");

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

    static readonly byte[] LF_BYTES = { 0x0A };

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

            if ((buf[0] & 0x08) == 0x08) // offline
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


    public ESCPOSSerialPrintDriver TT(string v)
    {
        var b = ToGbk(v);
        port.Write(b, 0, b.Length);
        return this;
    }


    public ESCPOSSerialPrintDriver HT()
    {
        port.Write("\t");

        return this;
    }

    public ESCPOSSerialPrintDriver LF()
    {
        port.Write("\n");

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


    public byte[] ToGbk(string text)
    {
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        return Encoding.GetEncoding("GBK").GetBytes(text);
    }
}