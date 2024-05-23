using System;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;

// ReSharper disable SpecifyACultureInStringConversionExplicitly

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


    public override void Rebind()
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

    static readonly byte[] DLE_EOT_1 = { 0x10, 0x04, 1 };

    static readonly byte[] ESC_at = { 0x1B, 0x40 };

    static readonly byte[] buf = new byte[128];

    public bool TryGetStatus(out byte result, int milliseconds)
    {
        result = default;

        semaph.Wait(milliseconds);
        try
        {
            INIT();

            Thread.Sleep(period);

            port.Write(DLE_EOT_1, 0, DLE_EOT_1.Length);

            Thread.Sleep(period);

            Array.Clear(buf);
            var num = port.Read(buf, 0, 8);
            if (num <= 0)
            {
                status = STU_ERR;
                return false;
            }

            result = buf[0];
            if ((result & 0x08) == 0x08) // offline
            {
                status = STU_VOID;
                return false;
            }
            else
            {
                status = STU_READY;
            }
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


    public ESCPOSSerialPrintDriver INIT()
    {
        port.Write(ESC_at, 0, ESC_at.Length);

        return this;
    }

    static readonly byte[]
        CHAR_0 = { 0x1d, 0x21, 0x00 },
        CHAR_1 = { 0x1d, 0x21, 0x11 },
        CHAR_2 = { 0x1d, 0x21, 0x22 },
        CHAR_3 = { 0x1d, 0x21, 0x33 },
        CHAR_4 = { 0x1d, 0x21, 0x44 };

    public ESCPOSSerialPrintDriver CHARSIZE(short n = 0)
    {
        switch (n)
        {
            case 0:
                port.Write(CHAR_0, 0, CHAR_0.Length);
                break;
            case 1:
                port.Write(CHAR_1, 0, CHAR_1.Length);
                break;
            case 2:
                port.Write(CHAR_2, 0, CHAR_2.Length);
                break;
            case 3:
                port.Write(CHAR_3, 0, CHAR_3.Length);
                break;
            case 4:
                port.Write(CHAR_4, 0, CHAR_4.Length);
                break;
        }

        return this;
    }


    static readonly byte[]
        JUST_0 = { 0x1b, 0x61, 0 },
        JUST_1 = { 0x1b, 0x61, 1 },
        JUST_2 = { 0x1b, 0x61, 2 };


    public ESCPOSSerialPrintDriver JUSTIFY(short n = 0)
    {
        switch (n)
        {
            case 0:
                port.Write(JUST_0, 0, JUST_0.Length);
                break;
            case 1:
                port.Write(JUST_1, 0, JUST_1.Length);
                break;
            case 2:
                port.Write(JUST_2, 0, JUST_2.Length);
                break;
        }

        return this;
    }

    public ESCPOSSerialPrintDriver FullCut()
    {
        port.Write("\u001Bi");

        return this;
    }

    public ESCPOSSerialPrintDriver PartialCut()
    {
        port.Write("\u001Bm");

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

    public ESCPOSSerialPrintDriver CR()
    {
        port.Write("\r");

        return this;
    }

    public ESCPOSSerialPrintDriver T(string v)
    {
        if (v != null)
        {
            port.Write(v);
        }

        return this;
    }


    private static readonly string[] PlaceHolder = { "0.00", "00.00", "000.00", "0000.00", "00000.00", "000000.00", "0000000.00", "00000000.00", "000000000.00", };

    public ESCPOSSerialPrintDriver T(decimal v, bool money = false)
    {
        port.Write(v.ToString("C"));

        return this;
    }

    //
    // static context
    //

    static ESCPOSSerialPrintDriver()
    {
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
    }

    public static byte[] ToGbk(string text)
    {
        return Encoding.GetEncoding("GBK").GetBytes(text);
    }
}