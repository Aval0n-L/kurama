using Kurama.Services.Interfaces;
using System.ComponentModel;

namespace Kurama.ViewModels;

public class VoiceAssistantViewModel : INotifyPropertyChanged
{
    private readonly ISpeechRecognitionService _speechRecognitionService;
    private readonly ISpeechSynthesisService _speechSynthesisService;

    private bool _isListening;

    public VoiceAssistantViewModel(
        ISpeechRecognitionService speechRecognitionService, 
        ISpeechSynthesisService speechSynthesisService)
    {
        _speechRecognitionService = speechRecognitionService;
        _speechSynthesisService = speechSynthesisService;

        _speechRecognitionService.OnSpeechRecognized += HandleSpeechRecognized;

        StartListening();
    }

    public bool IsListening
    {
        get => _isListening;
        set
        {
            _isListening = value;
            OnPropertyChanged(nameof(IsListening));
        }
    }

    private async void HandleSpeechRecognized(string recognizedText)
    {
        if (recognizedText.Equals("start", StringComparison.OrdinalIgnoreCase))
        {
            IsListening = true;
            await _speechSynthesisService.SpeakAsync("Listening started");
        }
        else if (recognizedText.Equals("stop", StringComparison.OrdinalIgnoreCase))
        {
            IsListening = false;
            await _speechSynthesisService.SpeakAsync("Listening stopped");
            StopListening();
        }
        else if (IsListening)
        {
            // Process the recognized command
            await _speechSynthesisService.SpeakAsync($"You said: {recognizedText}");
        }
    }

    public void StartListening()
    {
        _speechRecognitionService.StartRecognition();
    }

    public void StopListening()
    {
        _speechRecognitionService.StopRecognition();
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
