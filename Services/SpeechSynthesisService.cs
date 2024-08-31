using System.Speech.Synthesis;
using Kurama.Services.Interfaces;

namespace Kurama.Services;

public class SpeechSynthesisService : ISpeechSynthesisService
{
    // SpeechSynthesizer - Used to read text out loud.
    private readonly SpeechSynthesizer _synthesizer;

    public SpeechSynthesisService()
    {
        _synthesizer = new SpeechSynthesizer();
        _synthesizer.SetOutputToDefaultAudioDevice();
        _synthesizer.SelectVoiceByHints(VoiceGender.Male);
    }

    /*
     * Task.Run
     * Used to perform speech synthesis in a background thread so as not to block the main UI thread.
     */

    public async Task SpeakAsync(string text)
    {
        await Task.Run(() => _synthesizer.Speak(text));
    }

    public async Task SpeakAsyncCancelAll()
    {
        await Task.Run(() => _synthesizer.SpeakAsyncCancelAll());
    }
}