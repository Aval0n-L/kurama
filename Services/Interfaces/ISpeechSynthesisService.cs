namespace Kurama.Services.Interfaces;

public interface ISpeechSynthesisService
{
    Task SpeakAsync(string text);
    Task SpeakAsyncCancelAll();
}