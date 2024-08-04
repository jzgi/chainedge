using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Windows.Media.Imaging;
using ABI.Windows.Media.Capture;
using SIPSorcery.Net;
using SIPSorceryMedia.Abstractions;
using SIPSorceryMedia.Encoders;
using SIPSorceryMedia.Windows;
using Image = System.Windows.Controls.Image;

namespace ChainEdge.Source.Drivers;

public class VideoCallDriver : Driver
{
    private MediaCapture mc;

    private System.Windows.Controls.Image _picBox;

    protected internal override async void OnCreate(object state)
    {
        return;


        _picBox = new Image() { Width = 160 };
        _picBox.Source = new BitmapImage(new Uri("./static/favicon.ico", UriKind.Relative));
        Children.Add(_picBox);

        var videoEP = new WindowsVideoEndPoint(new VpxVideoEncoder());
        videoEP.RestrictFormats(format => format.Codec == VideoCodecsEnum.VP8);

        await WindowsVideoEndPoint.ListDevicesAndFormats();

        await videoEP.InitialiseVideoSourceDevice();

        videoEP.OnVideoSinkDecodedSampleFaster += (RawImage rawImage) =>
        {
            Dispatcher.InvokeAsync(() =>
            {
                if (rawImage.PixelFormat == VideoPixelFormatsEnum.Rgb)
                {
                    if (_picBox.Width != rawImage.Width || _picBox.Height != rawImage.Height)
                    {
                        _picBox.Width = rawImage.Width;
                        _picBox.Height = rawImage.Height;
                    }

                    Bitmap bmpImage = new Bitmap(rawImage.Width, rawImage.Height, rawImage.Stride, PixelFormat.Format24bppRgb, rawImage.Sample);
                    var stream = new MemoryStream();
                    bmpImage.Save(stream, ImageFormat.MemoryBmp);
                    var img = new BitmapImage() { StreamSource = stream };
                    _picBox.Source = img;
                }
            });
        };

        videoEP.OnVideoSinkDecodedSample += (byte[] bmp, uint width, uint height, int stride, VideoPixelFormatsEnum pixelFormat) =>
        {
            Dispatcher.InvokeAsync(() =>
            {
                if (pixelFormat == VideoPixelFormatsEnum.Rgb)
                {
                    if (_picBox.Width != (int)width || _picBox.Height != (int)height)
                    {
                        _picBox.Width = (int)width;
                        _picBox.Height = (int)height;
                    }

                    unsafe
                    {
                        fixed (byte* s = bmp)
                        {
                            Bitmap bmpImage = new Bitmap((int)width, (int)height, (int)(bmp.Length / height), PixelFormat.Format24bppRgb, (IntPtr)s);
                            var stream = new MemoryStream();
                            bmpImage.Save(stream, ImageFormat.MemoryBmp);
                            var img = new BitmapImage() { StreamSource = stream };
                            _picBox.Source = img;
                        }
                    }
                }
            });
        };

        await videoEP.StartVideo();


        RTCConfiguration config = new RTCConfiguration
        {
            // iceServers = new List<RTCIceServer> { new RTCIceServer { urls = STUN_URL } },
            X_UseRtpFeedbackProfile = true
        };
        var pc = new RTCPeerConnection(config);

        // Add local receive only tracks. This ensures that the SDP answer includes only the codecs we support.
        MediaStreamTrack audioTrack = new MediaStreamTrack(SDPMediaTypesEnum.audio, false, new List<SDPAudioVideoMediaFormat> { new SDPAudioVideoMediaFormat(SDPWellKnownMediaFormatsEnum.PCMU) }, MediaStreamStatusEnum.RecvOnly);
        pc.addTrack(audioTrack);

        MediaStreamTrack videoTrack = new MediaStreamTrack(videoEP.GetVideoSinkFormats(), MediaStreamStatusEnum.RecvOnly);
        //MediaStreamTrack videoTrack = new MediaStreamTrack(new VideoFormat(96, "VP8", 90000, "x-google-max-bitrate=5000000"), MediaStreamStatusEnum.RecvOnly);
        pc.addTrack(videoTrack);

        pc.OnVideoFrameReceived += videoEP.GotVideoFrame;
        pc.OnVideoFormatsNegotiated += (formats) => videoEP.SetVideoSinkFormat(formats.First());

        pc.onconnectionstatechange += async (state) =>
        {
            if (state == RTCPeerConnectionState.failed)
            {
                pc.Close("ice disconnection");
            }
            else if (state == RTCPeerConnectionState.closed)
            {
                await videoEP.CloseVideo();
            }
        };

        var sdp = pc.createOffer(new RTCOfferOptions());


        bound = true;
    }

    public override string Label => "视聊";

    public override void Bind()
    {
    }
}