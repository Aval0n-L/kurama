using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Kurama.Services.Interfaces;

namespace Kurama.ViewModels;

public partial class VoiceAssistantViewModel : ObservableObject
{
    private readonly ISpeechRecognitionService _speechRecognitionService;
    private readonly ISpeechSynthesisService _speechSynthesisService;
    private readonly IGlossaryService _glossaryService;

    [ObservableProperty]
    private string? _recognitionText;

    public VoiceAssistantViewModel(
        ISpeechRecognitionService speechRecognitionService, 
        ISpeechSynthesisService speechSynthesisService,
        IGlossaryService glossaryService)
    {
        _speechRecognitionService = speechRecognitionService;
        _speechSynthesisService = speechSynthesisService;
        _glossaryService = glossaryService;

        _speechRecognitionService.OnSpeechRecognized += HandleSpeechRecognized;
    }

    private async void HandleSpeechRecognized(string recognizedText)
    {
//        RecognitionText = string.Empty;

        var glossary = await _glossaryService.GetGlossaryAsync();
        
        switch (recognizedText)
        {
            case "Hello":
            case "Stop Talking":
            case "Stop Listening":
                _speechSynthesisService.SpeakViaGlossaryAsync(recognizedText, glossary);
                RecognitionText += recognizedText + "   ";
                break;

            default:
                await _speechSynthesisService.SpeakAsync("I don't understand you, please repeat");
                RecognitionText += recognizedText + "   ";
                break;
        }
    }

    [RelayCommand]
    public async Task TestText()
    {
        RecognitionText = string.Empty;
        var recognizedText = "I don't understand you, please repeat";
        await _speechSynthesisService.SpeakAsync(recognizedText);
        RecognitionText += recognizedText + "   ";
    }


    [ObservableProperty]
    private bool _isReady;
    private bool CanStartListening() => _isReady;
    private bool CanStopListening() => !_isReady;

    [RelayCommand(CanExecute = nameof(CanStartListening))]
    public void StartListening()
    {
        _speechRecognitionService.StartRecognition();
        _isReady = false;

        StopListeningCommand.NotifyCanExecuteChanged();
        StartListeningCommand.NotifyCanExecuteChanged();
    }

    [RelayCommand(CanExecute = nameof(CanStopListening))]
    public void StopListening()
    {
        _speechRecognitionService.StopRecognition();
        _isReady = true;

        StopListeningCommand.NotifyCanExecuteChanged();
        StartListeningCommand.NotifyCanExecuteChanged();
    }
}
