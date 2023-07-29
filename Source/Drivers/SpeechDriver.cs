using System.Globalization;
using System.Speech.Synthesis;
using ChainEdge.Features;

namespace ChainEdge.Drivers;

public class SpeechDriver : Driver, ISpeech
{
    private SpeechSynthesizer synth;

    public SpeechDriver()
    {
        synth = new SpeechSynthesizer();
        var d = synth.GetInstalledVoices();
        synth.SelectVoiceByHints(VoiceGender.Female, VoiceAge.Adult, 1, CultureInfo.CurrentCulture);
        synth.SetOutputToDefaultAudioDevice();

        synth.SpeakAsync("新单提示：眼睛超市、月亮湾");
    }

    public override void OnInitialize()
    {
        synth = new SpeechSynthesizer();
    }

    public override void Test()
    {
        synth.SetOutputToDefaultAudioDevice();

        synth.SpeakAsync("市场朋友们，请注意");
    }

    public void Speak(string v)
    {
        synth.SetOutputToDefaultAudioDevice();

        synth.SpeakAsync(v);
    }
}