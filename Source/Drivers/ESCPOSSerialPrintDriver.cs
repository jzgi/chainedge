using System;
using System.IO.Ports;
using System.Text;
using System.Threading;

// ReSharper disable SpecifyACultureInStringConversionExplicitly

namespace ChainEdge.Drivers;

public class ESCPOSSerialPrintDriver : Driver
{
    static readonly int[] BaudRates = { 9600, 19200 };

    readonly SemaphoreSlim semaph = new(1);

    SerialPort port;

    int cur;

    protected internal override void OnCreate(object state)
    {
        port = new()
        {
            Parity = Parity.None,
            DataBits = 8,
            StopBits = StopBits.One,
            ReadTimeout = Period,
            WriteTimeout = Period,
        };
    }

    public override bool IsBound => port.IsOpen && bound;

    public override void Bind()
    {
        // try to clear current states
        try
        {
            bound = false;

            if (port.IsOpen)
            {
                port.Close();
            }
        }
        catch (Exception e)
        {
        }

        // try each of the port names and baudrates
        //
        var names = SerialPort.GetPortNames();
        foreach (var name in names)
        {
            foreach (var rate in BaudRates)
            {
                semaph.Wait();
                try
                {
                    port.PortName = name;
                    port.BaudRate = rate;
                    port.Open();

                    // check the status
                    if (TryGetInput(out _, Period))
                    {
                        bound = true;
                        return;
                    }

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
    }

    public override string Label => "票据打印";

    static readonly byte[] DLE_EOT_1 = { 0x10, 0x04, 1 };

    static readonly byte[] ESC_at = { 0x1B, 0x40 };

    static readonly byte[] buf = new byte[128];

    public override bool TryGetInput(out (decimal a, decimal b) result, int milliseconds)
    {
        result = default;

        semaph.Wait(milliseconds);
        try
        {
            INIT();

            Thread.Sleep(Period);

            port.Write(DLE_EOT_1, 0, DLE_EOT_1.Length);

            Thread.Sleep(Period);

            Array.Clear(buf);
            var num = port.Read(buf, 0, 8);
            if (num <= 0)
            {
                bound = false;
                return false;
            }

            byte a = buf[0];
            if ((a & 0x08) == 0x08) // offline
            {
                bound = false;
                return false;
            }
            else
            {
                result.a = 0;
                return true;
            }
        }
        catch (Exception e)
        {
            bound = false;
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

    private static readonly byte[]
        CHAR_00 = { 0x1d, 0x21, 0x00 },
        CHAR_01 = { 0x1d, 0x21, 0x01 },
        CHAR_02 = { 0x1d, 0x21, 0x02 },
        CHAR_10 = { 0x1d, 0x21, 0x10 },
        CHAR_11 = { 0x1d, 0x21, 0x11 },
        CHAR_12 = { 0x1d, 0x21, 0x12 },
        CHAR_20 = { 0x1d, 0x21, 0x20 },
        CHAR_21 = { 0x1d, 0x21, 0x21 },
        CHAR_22 = { 0x1d, 0x21, 0x22 };


    public ESCPOSSerialPrintDriver CHARSIZE(short n = 0)
    {
        switch (n)
        {
            case 0x00:
                port.Write(CHAR_00, 0, CHAR_00.Length);
                break;
            case 0x01:
                port.Write(CHAR_01, 0, CHAR_01.Length);
                break;
            case 0x02:
                port.Write(CHAR_02, 0, CHAR_02.Length);
                break;
            case 0x10:
                port.Write(CHAR_10, 0, CHAR_10.Length);
                break;
            case 0x11:
                port.Write(CHAR_11, 0, CHAR_11.Length);
                break;
            case 0x12:
                port.Write(CHAR_12, 0, CHAR_12.Length);
                break;
            case 0x20:
                port.Write(CHAR_20, 0, CHAR_20.Length);
                break;
            case 0x21:
                port.Write(CHAR_21, 0, CHAR_21.Length);
                break;
            case 0x22:
                port.Write(CHAR_22, 0, CHAR_22.Length);
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
        if (b != null)
        {
            port.Write(b, 0, b.Length);
        }
        return this;
    }

    public ESCPOSSerialPrintDriver HTPOS(byte p1, byte p2, byte p3, byte p4)
    {
        byte[] dat = { 0x1b, 0x44, p1, p2, p3, p4, 0 };

        port.Write(dat, 0, dat.Length);

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
        if (money)
        {
            port.Write(v.ToString("F2"));
        }
        else
        {
            port.Write(v.ToString());
        }

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
        if (text == null)
        {
            return null;
        }

        return Encoding.GetEncoding("GBK").GetBytes(text);
    }
}