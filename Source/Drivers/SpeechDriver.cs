using System.Globalization;
using System.Speech.Synthesis;
using ChainEdge.Jobs;
using ChainFX;

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

        status = STU_READY;
    }

    public override void Rebind()
    {
    }

    public override string Label => "语音";

    public void Speak(string v)
    {
        synth.Speak(v);
    }
}