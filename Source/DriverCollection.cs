using System.Collections.Generic;

namespace SkyTerm
{
    public class DriverCollection : List<Driver>
    {
        private int active;

        public int Active => active;
    }
}