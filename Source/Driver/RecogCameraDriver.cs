using System;
using DirectShowLib;
using SkyGate.Feature;

namespace SkyGate.Driver
{
    public class RecogCameraDriver : Driver, IRecog
    {
        private IVideoWindow capture;

        public override void Test()
        {
            throw new NotImplementedException();
        }

        public double Loss(float output, float label)
        {
            throw new NotImplementedException();
        }

        public float Derivative(float output, float label)
        {
            throw new NotImplementedException();
        }

        public int GetItemIdByScan()
        {
            throw new NotImplementedException();
        }

        public int GetNumberByScan()
        {
            throw new NotImplementedException();
        }
    }
}