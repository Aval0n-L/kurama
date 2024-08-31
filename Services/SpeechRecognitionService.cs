using Kurama.Services.Interfaces;
using System.Speech.Recognition;

namespace Kurama.Services;

public class SpeechRecognitionService : ISpeechRecognitionService
{
    // SpeechRecognitionEngine - This class is used to handle voice input.
    private readonly SpeechRecognitionEngine _recognitionEngine;

    public event Action<string>? OnSpeechRecognized;

    public SpeechRecognitionService()
    {
        _recognitionEngine = new SpeechRecognitionEngine();
        _recognitionEngine.SetInputToDefaultAudioDevice();

        // We set up acceptable voice commands that our service can recognize.
        var choices = new Choices(new[] { "start", "stop", "hello", "how are you" });
        var grammar = new Grammar(new GrammarBuilder(choices));
        _recognitionEngine.LoadGrammar(grammar);

        _recognitionEngine.SpeechRecognized += (s, e) =>
        {
            OnSpeechRecognized?.Invoke(e.Result.Text);
        };
    }

    public void StartRecognition()
    {
        _recognitionEngine.RecognizeAsync(RecognizeMode.Multiple);
    }

    public void StopRecognition()
    {
        _recognitionEngine.RecognizeAsyncStop();
    }
}