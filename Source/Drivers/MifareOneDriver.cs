using System;
using System.Diagnostics;
using System.IO.Ports;
using System.Threading;

namespace ChainEdge
{
    public class MifareOneDriver : Driver
    {
        const int BUFFER = 32;

        readonly SemaphoreSlim semaph = new(1);

        SerialPort port;

        protected internal override void OnCreate(object state)
        {
            port = new()
            {
                BaudRate = 9600,
                DataBits = 8,
                Parity = Parity.None,
                StopBits = StopBits.One,
                ReadTimeout = 100,
                WriteTimeout = 100
            };
        }

        public override void Rebind()
        {
            foreach (var name in SerialPort.GetPortNames())
            {
                semaph.Wait();
                try
                {
                    // port.PortName = name;
                    // port.Open();
                    // if (status <= STU_VOID)
                    // {
                    //     port.Close(); // continue
                    // }
                    // else
                    // {
                    //     break; // keep COM port
                    // }
                }
                catch (UnauthorizedAccessException e) // used by other process
                {
                }
                catch (InvalidOperationException e) // port is open
                {
                    // port.Close();
                }
                catch (Exception e)
                {
                    if (port.IsOpen)
                    {
                        // port.Close();
                    }
                }
                finally
                {
                    semaph.Release();
                }
            }
        }

        public override string Label => "智能卡";

        public void Open()
        {
            port.Open();

            // set device baudrate 
            InitializePort(port.BaudRate);
        }

        public void Close()
        {
            port.Close();
        }

        // one-byte buffer
        readonly byte[] ZERO = { 0x00 };

        /// <summary>
        ///  Send a command to device 0
        /// </summary>
        /// <remarks>Format: Head (2) + Length (2,3) + Node ID (4,5) + Function Code (6,7) + [Data] + XOR (1)</remarks>
        /// <param name="code">command code</param>
        /// <param name="data">data area, can be null</param>
        void SendCommand(ushort code, byte[] data)
        {
            byte[] buf = new byte[BUFFER];

            // head
            buf[0] = 0xaa;
            buf[1] = 0xbb; // fixed

            // desination node id
            buf[4] = 0x00;
            buf[5] = 0x00; // 0x0000 to broadcast to each reader

            // function code
            buf[6] = (byte)(code & 0xff);
            buf[7] = (byte)(code >> 8);

            // data area
            int datalen = 0;
            if (data != null && (datalen = data.Length) != 0)
            {
                Array.Copy(data, 0, buf, 8, datalen);
            }

            // xor check (from nodeid through data)
            int xorpos = 8 + datalen; // position of check byte
            byte xor = 0;
            for (int i = 4; i < xorpos; i++)
            {
                xor ^= buf[i];
            }
            buf[xorpos] = xor;

            // length ( from node to xor)
            int length = 5 + datalen;
            buf[2] = (byte)(length & 0xff);
            buf[3] = (byte)(length >> 8);

            //
            // write to the serial port
            int cmdlen = xorpos + 1;
            for (int i = 0; i < cmdlen; i++)
            {
                port.Write(buf, i, 1);
                if (buf[i] == 0xaa && i > 1) // escape AA by adding a 00
                {
                    port.Write(ZERO, 0, 1);
                }
            }
        }

        byte ReceiveReply(ushort code, out byte[] data)
        {
            byte[] buf = new byte[BUFFER];
            data = null;

            int p = 0;
            int b;
            while ((b = port.ReadByte()) != -1)
            {
                if (b == 0xaa && port.ReadByte() == 0xbb)
                {
                    buf[p++] = 0xaa;
                    buf[p++] = 0xbb;
                    break;
                }
            }

            if (p != 2)
            {
                return 32;
            }

            // receive up the package
            if (!ReceiveBytes(2, buf, ref p))
            {
                return 45;
            }
            int length = buf[2] + (buf[3] << 8);
            if (!ReceiveBytes(length, buf, ref p))
            {
                return 45;
            }

            // verify function code
            if (code != (ushort)(buf[6] + (buf[7] << 8)))
            {
                return 43;
            }

            // check xor integrity
            int xorpos = p - 1;
            byte xor = 0;
            for (int i = 4; i < xorpos; i++)
            {
                xor ^= buf[i];
            }
            if (buf[xorpos] != xor)
            {
                return 23;
            }

            // get the data field, if any
            int datalen = length - 6;
            if (datalen > 0)
            {
                data = new byte[datalen];
                Array.Copy(buf, 9, data, 0, datalen);
            }

            // return the status
            return buf[8];
        }

        bool ReceiveBytes(int count, byte[] buf, ref int p)
        {
            int b;
            while (count > 0 && (b = port.ReadByte()) != -1)
            {
                if (b == 0xaa && port.ReadByte() == -1)
                {
                    return false;
                }
                buf[p++] = (byte)b;
                count--;
            }
            return true;
        }

        public byte InitializePort(int baud)
        {
            byte v =
                baud >= 115200 ? (byte)7 :
                baud >= 57600 ? (byte)6 :
                baud >= 38400 ? (byte)5 :
                baud >= 28800 ? (byte)4 :
                baud >= 19200 ? (byte)3 :
                baud >= 14400 ? (byte)2 :
                (byte)1; // 9600

            SendCommand(0x0101, new[] { v });

            byte[] data;
            return ReceiveReply(0x0101, out data);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="delay">delay * 10ms</param>
        /// <returns></returns>
        public byte SetBuzzerBeep(byte delay)
        {
            SendCommand(0x0106, new[] { delay });

            byte[] data;
            return ReceiveReply(0x0106, out data);
        }

        public byte SetLedColor(bool red, bool green)
        {
            byte v = (byte)((red ? 0x01 : 0x00) | (green ? 0x02 : 0x00));

            SendCommand(0x0107, new[] { v });

            byte[] data;
            return ReceiveReply(0x0107, out data);
        }


        public byte SetAntennaStatus(bool open)
        {
            byte v = open ? (byte)0x01 : (byte)0x00;

            SendCommand(0x010c, new[] { v });

            byte[] data;
            return ReceiveReply(0x010c, out data);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="all">true means all type A cards in field, false idle cards</param>
        /// <param name="tagtype"></param>
        /// <returns>a byte of status</returns>
        public byte Request(bool all, out string tagtype)
        {
            byte v = all ? (byte)0x52 : (byte)0x26;

            SendCommand(0x0201, new[] { v });

            byte[] type;
            byte status = ReceiveReply(0x0201, out type);
            if (type != null)
            {
                ushort n = (ushort)(type[0] + (type[1] << 8));

                tagtype =
                    n == 0x4400 ? "ultralight" :
                    n == 0x0400 ? "mifare one S50" :
                    n == 0x0200 ? "mifare one S70" :
                    n == 0x4403 ? "mifare desfire" :
                    n == 0x0800 ? "mifare pro" :
                    n == 0x0403 ? "mifare prox" :
                    "unkown";
            }
            else
            {
                tagtype = null;
            }

            return status;
        }

        public byte Anticollision(out uint cardsn)
        {
            SendCommand(0x0202, null);

            byte[] data;
            byte status = ReceiveReply(0x0202, out data);

            cardsn = (uint)(data[0] + (data[1] << 8) + (data[2] << 16) + (data[3] << 24));
            return status;
        }

        public byte Select(uint cardsn)
        {
            byte[] v = { (byte)(cardsn & 0xff), (byte)(cardsn >> 8 & 0xff), (byte)(cardsn >> 16 & 0xff), (byte)(cardsn >> 24 & 0xff) };
            SendCommand(0x0203, v);

            byte[] data;
            return ReceiveReply(0x0203, out data);
        }

        public byte Halt(short icdev)
        {
            return 0;
        }

        public byte Authentication2(bool keyA, byte block, byte[] key)
        {
            byte[] v = new byte[8];
            v[0] = keyA ? (byte)0x60 : (byte)0x61;
            v[1] = block;
            Array.Copy(key, 0, v, 2, key.Length);

            SendCommand(0x0207, v);

            byte[] data;
            return ReceiveReply(0x0207, out data);
        }

        public byte Read(byte block, out byte[] data)
        {
            SendCommand(0x0208, new[] { block });

            return ReceiveReply(0x0208, out data);
        }

        public byte Write(byte block, byte[] data)
        {
            SendCommand(0x0209, new[] { block });

            byte[] ret;
            return ReceiveReply(0x0209, out ret);
        }
    }
}