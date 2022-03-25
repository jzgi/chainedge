using SkyGate;

namespace SkyGate.Manner
{
    /// <summary>
    /// 
    /// </summary>
    public class Manner : IKeyable<short>
    {
        static Map<short, Manner> All = new Map<short, Manner>()
        {
            new Manner(), //
            // serial port
            // zigbee
            // screen
            // camera
        };

        private short id;

        public short Key => id;
    }
}