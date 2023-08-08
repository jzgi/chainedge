using System.Globalization;
using System.Speech.Synthesis;

namespace ChainEdge.Drivers;

public class SpeechDriver : Driver
{
    readonly SpeechSynthesizer synth;

    public SpeechDriver()
    {
        synth = new SpeechSynthesizer();
        synth.SelectVoiceByHints(VoiceGender.Female, VoiceAge.Adult, 1, CultureInfo.CurrentCulture);
        synth.SetOutputToDefaultAudioDevice();

    }

    public override void OnInitialize()
    {
        synth.SetOutputToDefaultAudioDevice();
    }

    public override void Test()
    {

    }

    public void Speak(string v)
    {
        synth.SpeakAsync(v);
    }
}