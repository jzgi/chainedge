﻿using System.Runtime.InteropServices;
using ChainEdge.Features;

namespace ChainEdge.Profiles
{
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [ComVisible(true)]
    public class GreenhouseProfile : Profile
    {
        public GreenhouseProfile() : base("greenh", "种植温室",
            new[]
            {
                typeof(ISoil)
            }
        )
        {
        }
    }
}