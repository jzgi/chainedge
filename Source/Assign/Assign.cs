namespace SkyGate.Assign
{
    /// <summary>
    /// To keep record for resource asslignments.
    /// </summary>
    public class Assign : IKeyable<short>
    {
        static Map<short, Assign> All = new Map<short, Assign>()
        {
            new Assign(), //
            // serial port
            // zigbee
            // screen
            // camera
        };

        private short id;

        public short Key => id;
    }
}