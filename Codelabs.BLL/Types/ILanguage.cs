namespace Codelabs.BLL.Types;

public interface ILanguage
{
    public string Name { get; }
    public bool IsCompilable { get; }
    
    public string ImageName { get; }
    public string Filename { get; }
    public string CompileCommand { get; }
    public string RunCommand { get; }
}