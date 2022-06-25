using System;
using CoEdge.Features;
using DirectShowLib;

namespace CoEdge.Drivers
{
    public class RecognizerCameraDriver : Driver, IRecognizer
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