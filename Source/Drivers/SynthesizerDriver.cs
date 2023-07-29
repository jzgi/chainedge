using System.Speech.Synthesis;
using ChainEdge.Features;

namespace ChainEdge.Drivers;

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

    public void Speak(string v)
    {
        synthesizer.SetOutputToDefaultAudioDevice();

        synthesizer.SpeakAsync(v);
    }
}