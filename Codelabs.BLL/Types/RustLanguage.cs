namespace Codelabs.BLL.Types;

public readonly record struct RustLanguage : ILanguage
{
    public string Name => "rust";
    public bool IsCompilable => true;

    public string ImageName => "rust-image";
    public string Filename => "main.rs";
    public string CompileCommand => "rustc main.rs";
    public string RunCommand => "./main";
}