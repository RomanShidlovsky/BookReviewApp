using Book.Domain.Entities;
using Book.Domain.Interfaces.Repositories;

namespace Book.Infrastructure.Seed;

public class LanguageSeedInitializer(ILanguageRepository _languageRepository) : ISeedInitializer
{
    public static Language[] Languages { get; } =
    [
        new Language() { Name = "russian" },
        new Language() { Name = "belorussian" },
        new Language() { Name = "english" },
        new Language() { Name = "german" }
    ];

    public void Init()
    {
        foreach (var language in Languages)
        {
            _languageRepository.Create(language);
        }
    }
}