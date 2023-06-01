using System;
using ChainEdge.Features;
using System.Speech.Synthesis;

namespace ChainEdge.Drivers
{
    public class SynthesizerDriver : Driver, ISpeech
    {
        private SpeechSynthesizer synthesizer;

        public override void OnInitialize()
        {
            synthesizer = new SpeechSynthesizer();
        }

        public override void Test()
        {
            synthesizer.SetOutputToDefaultAudioDevice();

            synthesizer.SpeakAsync("市场朋友们，请注意");
        }
    }
}