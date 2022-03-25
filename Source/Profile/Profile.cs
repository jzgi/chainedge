using System;

namespace SkyGate.Profile
{
    /// <summary>
    /// A set of configurations and processing logics for a given purpose or circumstance.
    /// </summary>
    public class Profile : IKeyable<string>
    {
        /// 
        /// All supported profiles
        /// 
        public static Map<string, Profile> All = new Map<string, Profile>()
        {
            new WeighProfile(),
            new WorkstProfile()
        };


        //
        // instance states
        //

        readonly string name;

        readonly string tip;

        readonly Type[] features;


        // tested drivers for actually-running devices
        private Map<string, Driver> drivers;

        public Profile(string name, string tip, Type[] features)
        {
            this.name = name;
            this.tip = tip;
            this.features = features;
        }

        public string Key => name;
    }
}