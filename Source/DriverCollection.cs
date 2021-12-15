using System.Collections.Generic;

namespace SkyEdge
{
    public class DriverCollection : List<Driver>
    {
        private int active;

        public int Active => active;
    }
}