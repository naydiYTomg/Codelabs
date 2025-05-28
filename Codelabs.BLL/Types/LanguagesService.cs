namespace Codelabs.BLL.Types;

public class LanguagesService
{
    private readonly Dictionary<string, ILanguage> _languages = new();

    public void AddLanguage(ILanguage lang)
    {
        var key = lang.Name.ToLower();
        if (!_languages.TryAdd(key, lang))
            throw new InvalidOperationException($"language with name '{key}' already registered");
    }
    public ILanguage GetLanguageUnchecked(string name) => _languages[name.ToLower()];
    public bool TryGetLanguage(string name, out ILanguage language) => _languages.TryGetValue(name, out language);
    public IEnumerable<ILanguage> GetLanguages() => _languages.Values;

}