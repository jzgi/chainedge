using System;

namespace Edgely.Drivers
{
    public class DriverDescriptor : IKeyable<string>
    {
        static Map<string, DriverDescriptor> All = new Map<string, DriverDescriptor>()
        {
            new DriverDescriptor(
                "pcscale",
                "智能台秤",
                typeof(PcScaleDriver),
                1
            ),
            new DriverDescriptor(
            "pcscale",
            "智能台秤",
            typeof(PcScaleDriver),
            1
            )
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