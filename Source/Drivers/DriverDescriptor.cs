using System;
using System.Collections.Generic;
using ChainFx;

namespace ChainEdge.Drivers
{
    public class DriverDescriptor
    {
        static Dictionary<string, DriverDescriptor> All = new ()
        {
            // new DriverDescriptor(
            //     "pcscale",
            //     "智能台秤",
            //     typeof(PcScaleDriver),
            //     1
            // ),
            // new DriverDescriptor(
            //     "pcscale",
            //     "智能台秤",
            //     typeof(PcScaleDriver),
            //     1
            // )
        };

        //
        // instance states
        //

        // identifier
        string name;

        // descriptive text
        string tip;

        // the declaration class for the driver
        Type @class;

        // how many instances allowed
        short cardinality;

        DriverDescriptor(string name, string tip, Type @class, short cardinality)
        {
            this.name = name;
            this.tip = tip;
            this.@class = @class;
            this.cardinality = cardinality;
        }

        public string Key => name;
    }
}