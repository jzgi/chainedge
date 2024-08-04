using System;

namespace ChainEdge;

public static class Program
{
    public static void _Main()
    {
        // if(UPnP.IsAvailable)
        // {
        //     Console.WriteLine($"It worked! Local: {UPnP.LocalIP}, External:{UPnP.ExternalIP}");
        // }
        // else
        // {
        //     Console.WriteLine("It failed");
        // }

        const ushort port = 29000;
        var open = GatewayDeviceUtility.IsOpen(Protocol.TCP, port);

        PrintMappings();

        Console.WriteLine($"IsOpen({Protocol.TCP},{port}) --> {GatewayDeviceUtility.IsOpen(Protocol.TCP, port)}");

        Console.WriteLine("Opening Port");
        GatewayDeviceUtility.Open(Protocol.TCP, port);
        Console.WriteLine("Opened");

        open = GatewayDeviceUtility.IsOpen(Protocol.TCP, port);

        Console.WriteLine($"IsOpen({Protocol.TCP},{port}) --> {GatewayDeviceUtility.IsOpen(Protocol.TCP, port)}");

        PrintMappings();

        // Console.ReadKey();
        Console.WriteLine("Closing Port");
        GatewayDeviceUtility.Close(Protocol.TCP, port);
        Console.WriteLine("Closed");
    }


    private static void PrintMappings()
    {
        try
        {
            for (int i = 0;; i++)
            {
                try
                {
                    var ret = GatewayDeviceUtility.GetGenericPortMappingEntry(i);
                    if (ret.Count == 0)
                    {
                        break;
                    }

                    foreach (var e in ret)
                    {
                        EdgeApp.War(e.ToString());
                    }
                }
                catch
                {
                    break;
                }
            }
        }
        catch (Exception e)
        {
            EdgeApp.War(e.Message);
        }
    }
}