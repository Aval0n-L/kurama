namespace Kurama.Services.Interfaces;


/// <summary>
/// Service to work with Speech-To-Text
/// </summary>
public interface ISpeechRecognitionService
{
    Task InitializeAsync();
    void StartRecognition();
    void StopRecognition();

    event Action<string> OnSpeechRecognized;
}