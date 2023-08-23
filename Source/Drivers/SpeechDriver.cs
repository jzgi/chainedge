using System.Globalization;
using System.Speech.Synthesis;
using System.Text;

namespace ChainEdge.Drivers;

public class SpeechDriver : Driver
{
    readonly SpeechSynthesizer synth;

    public SpeechDriver()
    {
        synth = new SpeechSynthesizer();
        synth.SelectVoiceByHints(VoiceGender.Female, VoiceAge.Adult, 1, CultureInfo.CurrentCulture);
        synth.SetOutputToDefaultAudioDevice();
        synth.Volume = 100;
    }

    public override void Reset()
    {
    }

    public override string Label => "语音";

    public void Speak(string v)
    {
        synth.SpeakAsync(v);
    }
}