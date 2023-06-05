using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Controls;
using ChainEdge.Features;
using FlashCap;
using Image = System.Drawing.Image;

namespace ChainEdge.Drivers
{
    public class ObjectDetectorDriver : Driver, IDetect
    {
        
        // UI
        
        // captured image display
        private Image image;
        
        // camera selection
        private ComboBox devices;
        
        
        public override void Test()
        {
            // Capture device enumeration:
            var devices = new CaptureDevices();

            foreach (var descriptor in devices.EnumerateDescriptors())
            {
                // "Logicool Webcam C930e: DirectShow device, Characteristics=34"
                // "Default: VideoForWindows default, Characteristics=1"
                Console.WriteLine(descriptor);

                foreach (var characteristics in descriptor.Characteristics)
                {
                    // "1920x1080 [JPEG, 30fps]"
                    // "640x480 [YUYV, 60fps]"
                    Console.WriteLine(characteristics);
                }
            }
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
            return 0;
        }

        public async Task ByScan()
        {
            var devices = new CaptureDevices();

// Open a device with a video characteristics:
            var descriptor0 = devices.EnumerateDescriptors().ElementAt(0);

            await using var device = await descriptor0.OpenAsync(
                descriptor0.Characteristics[0], bufferScope =>
                {
                    // Captured into a pixel buffer from an argument.

                    // Get image data (Maybe DIB/JPEG/PNG):
                    byte[] image = bufferScope.Buffer.ExtractImage();

                    // Anything use of it...
                    var ms = new MemoryStream(image);
                    var bitmap = Bitmap.FromStream(ms);
                    return Task.CompletedTask;

                    // ...
                });

// Start processing:
            await device.StartAsync();
        }

        public int GetNumberByScan()
        {
            throw new NotImplementedException();
        }
    }
}