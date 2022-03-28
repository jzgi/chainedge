namespace SkyGate.Allocators
{
    /// <summary>
    /// To keep record for resource asslignments.
    /// </summary>
    public class Allocator : IKeyable<short>
    {
        static Map<short, Allocator> All = new Map<short, Allocator>()
        {
            new Allocator(), //
            // serial port
            // zigbee
            // screen
            // camera
        };

        private short id;

        public short Key => id;
    }
}