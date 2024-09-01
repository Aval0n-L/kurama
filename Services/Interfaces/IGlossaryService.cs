using Kurama.Models;

namespace Kurama.Services.Interfaces;

public interface IGlossaryService
{
    string[] GetGlossaryByKey(IEnumerable<Glossary> glossary, string key);

    Task<IEnumerable<Glossary>> GetGlossaryAsync();
}