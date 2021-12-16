using System;

namespace SkyEdge.Driver
{
    public class CameraRecognizerDriver : _Driver, IRecognition
    {
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

        public int getItemIdByScan()
        {
            throw new NotImplementedException();
        }

        public int getNumberByScan()
        {
            throw new NotImplementedException();
        }
    }
}