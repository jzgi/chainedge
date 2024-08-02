using System.Globalization;
using System.Speech.Synthesis;

namespace ChainEdge.Drivers;

public class SpeechDriver : Driver
{
    SpeechSynthesizer synth;

    protected internal override void OnCreate(object state)
    {
        synth = new SpeechSynthesizer();
        synth.SelectVoiceByHints(VoiceGender.Female, VoiceAge.Adult, 1, CultureInfo.CurrentCulture);
        synth.SetOutputToDefaultAudioDevice();
        synth.Volume = 100;

        bound = true;
    }

    public override void Bind()
    {
    }

    public override string Label => "播报";

    public void Speak(string v)
    {
        synth.Speak(v);
    }
}