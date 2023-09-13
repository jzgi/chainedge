using System;
using System.IO.Ports;
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
                    if ((v & 0x08) == 0x08) // offline
                    {
                        status = STU_VOID;
                    }

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

    static readonly byte[] LF_BYTES = { 0x0A };

    static readonly byte[] buf = new byte[1024];

    public bool TryGetStatus(out byte result, int milliseconds)
    {
        result = default;

        semaph.Wait(milliseconds);
        try
        {
            Array.Clear(buf);

            port.Write(DLE_EOT_1, 0, DLE_EOT_1.Length);

            Thread.Sleep(period);

            var num = port.Read(buf, 0, 1);
            if (num != 1)
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


    public ESCPOSSerialPrintDriver INIT()
    {
        port.Write("\u001B@");

        return this;
    }

    public ESCPOSSerialPrintDriver CUT()
    {
        port.Write("\u001Bi");

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