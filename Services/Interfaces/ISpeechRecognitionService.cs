namespace Kurama.Services.Interfaces;

public interface ISpeechRecognitionService
{
    event Action<string> OnSpeechRecognized;
    void StartRecognition();
    void StopRecognition();
}