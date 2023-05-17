using System;
using ChainEdge;
using ChainEdge.Profiles;
using ChainFx;

namespace ChainEdge
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
            new PosProfile(),
            new WorkstationProfile()
        };


        //
        // instance states
        //

        readonly string name;

        readonly string tip;

        readonly Type[] features;


        //
        // tested drivers or driver sets for actually-running devices
        Map<string, Driver> drivers;

        public Profile(string name, string tip, Type[] features)
        {
            this.name = name;
            this.tip = tip;
            this.features = features;
        }

        public string Key => name;
    }
}