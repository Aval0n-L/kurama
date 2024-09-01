using Kurama.Models;

namespace Kurama.Services.Interfaces;

public interface ISpeechSynthesisService
{
    Task SpeakAsync(string text);
    void SpeakViaGlossaryAsync(string text, IEnumerable<Glossary> glossary);

    //event Action<double[]> OnAmplitudeChanged;
}