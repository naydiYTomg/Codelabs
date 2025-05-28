namespace Codelabs.BLL.Types;

public readonly record struct CppLanguage : ILanguage
{
    public string Name => "cpp";
    public bool IsCompilable => true;

    public string ImageName => "cpp-image";
    public string Filename => "main.cpp";
    public string CompileCommand => "g++ -o main main.cpp";
    public string RunCommand => "./main";
}