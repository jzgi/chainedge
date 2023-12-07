using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;
using Windows.Media.Capture;
using Windows.Media.MediaProperties;
using Windows.Storage.Streams;
using BitmapEncoder = Windows.Graphics.Imaging.BitmapEncoder;
using Image = System.Windows.Controls.Image;

namespace ChainEdge.Drivers;

public class WtcLiveChatDriver
{
}

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private Image Images1;

    public MainWindow()
    {
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        GetFrameAsync();
    }

    private async void GetFrameAsync()
    {
        var mediaCapture = new MediaCapture();
        mediaCapture.Failed += (obj, args) => MessageBox.Show(args.Message);
        await mediaCapture.InitializeAsync();

        var lowLagCapture = await mediaCapture.PrepareLowLagPhotoCaptureAsync(
            ImageEncodingProperties.CreateUncompressed(MediaPixelFormat.Bgra8));
        var capturedPhoto = await lowLagCapture.CaptureAsync();
        var softwareBitmap = capturedPhoto.Frame.SoftwareBitmap;
        await lowLagCapture.FinishAsync();
        using (var stream = new InMemoryRandomAccessStream())
        {
            var encoder = await BitmapEncoder.CreateAsync(
                BitmapEncoder.PngEncoderId, stream);
            encoder.SetSoftwareBitmap(softwareBitmap);
            await encoder.FlushAsync();
            Images1.Source = ToBitmapImage(new Bitmap(stream.AsStream()));
        }
    }

    private BitmapImage ToBitmapImage(Bitmap bitmap)
    {
        using (MemoryStream memory = new MemoryStream())
        {
            bitmap.Save(memory, ImageFormat.Bmp);
            memory.Seek(0, SeekOrigin.Begin);
            BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.StreamSource = memory;
            bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
            bitmapImage.EndInit();
            return bitmapImage;
        }
    }
}