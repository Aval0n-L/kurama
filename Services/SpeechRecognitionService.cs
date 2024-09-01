using Kurama.Services.Interfaces;
using System.Speech.Recognition;
using static System.Net.Mime.MediaTypeNames;

namespace Kurama.Services;

public class SpeechRecognitionService : ISpeechRecognitionService
{

    private readonly IGlossaryService _glossaryService;

    // SpeechRecognitionEngine - This class is used to handle voice input.
    private readonly SpeechRecognitionEngine _recognitionEngine;

    public event Action<string>? OnSpeechRecognized;

    public SpeechRecognitionService(IGlossaryService glossaryService)
    {
        _glossaryService = glossaryService;

        _recognitionEngine = new SpeechRecognitionEngine();
        _recognitionEngine.SetInputToDefaultAudioDevice();
    }

    // set up acceptable voice commands that our service can recognize.
    public async Task InitializeAsync()
    {
        var choices = new Choices();

        var glossary = await _glossaryService.GetGlossaryAsync();

        var keys = glossary.Select(x => x.Key).ToArray();
        choices.Add(keys);

        var grammar = new Grammar(new GrammarBuilder(choices));
        _recognitionEngine.LoadGrammar(grammar);

        _recognitionEngine.SpeechRecognized += (s, e) =>
        {
            OnSpeechRecognized?.Invoke(e.Result.Text);
        };
    }

    public void StartRecognition()
    {
        if (_recognitionEngine.Grammars.Count == 0)
        {
            throw new InvalidOperationException("No grammar loaded. Initialize the service first.");
        }

        _recognitionEngine.RecognizeAsync(RecognizeMode.Multiple);
    }

    public void StopRecognition()
    {
        _recognitionEngine.RecognizeAsyncStop();
    }
}