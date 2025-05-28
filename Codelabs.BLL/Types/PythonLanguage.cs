namespace Codelabs.BLL.Types;

public readonly record struct PythonLanguage : ILanguage
{
    public string Name => "python";
    public bool IsCompilable => false;

    public string ImageName => "py-image";
    public string Filename => "main.py";
    public string CompileCommand => throw new InvalidOperationException("cannot compile python");
    public string RunCommand => "python -u main.py";
}