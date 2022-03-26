using System;
using System.Runtime.InteropServices;

namespace SkyGate.Profile
{
    /// <summary>
    /// A set of configurations and processing logics for a given purpose or circumstance.
    /// </summary>
    /// <remarks>A profile exposes properties and methods to scripting environment</remarks>
    public class Profile : IKeyable<string>
    {
        /// 
        /// All supported profiles
        /// 
        public static Map<string, Profile> All = new Map<string, Profile>()
        {
            new PcScaleProfile(),
            new WorkstationProfile()
        };


        //
        // instance states
        //

        readonly string name;

        readonly string tip;

        readonly Type[] features;


        // tested drivers or driver sets for actually-running devices
        private Map<string, Driver.Driver> drivers;

        public Profile(string name, string tip, Type[] features)
        {
            this.name = name;
            this.tip = tip;
            this.features = features;
        }

        public string Key => name;
    }
}