using System.Text.Json;
using Kurama.Models;
using Kurama.Services.Interfaces;

namespace Kurama.Services;

public class GlossaryService : IGlossaryService
{
    public string[] GetGlossaryByKey(IEnumerable<Glossary> glossary, string key)
    {
        if (key == null) throw new ArgumentNullException(nameof(key));

        return glossary
            .Where(x => x.Key.Contains(key, StringComparison.OrdinalIgnoreCase))
            .SelectMany(x => x.Value)
            .ToArray();
    }

    public async Task<IEnumerable<Glossary>> GetGlossaryAsync()
    {
        //var filePath = "Resources/Raw/CustomGlossary.json";

        try
        {
            using var stream = await FileSystem.OpenAppPackageFileAsync("CustomGlossary.json");
            using var reader = new StreamReader(stream);
            var json = await reader.ReadToEndAsync();

            if (string.IsNullOrWhiteSpace(json))
            {
                throw new ArgumentNullException(nameof(json), "The JSON content is null or empty.");
            }

            return JsonSerializer.Deserialize<List<Glossary>>(json) ?? new List<Glossary>();
        }
        catch (JsonException jsonEx)
        {
            throw new InvalidOperationException("Failed to deserialize the glossary JSON.", jsonEx);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An unexpected error occurred while reading the glossary.", ex);
        }
    }
}
