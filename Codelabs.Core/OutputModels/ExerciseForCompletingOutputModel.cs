namespace Codelabs.Core.OutputModels;

public class ExerciseForCompletingOutputModel
{
    public required string Name { get; set; }
    public required string RequirementsPath { get; set; }
    public required string DesiredOutput { get; set; }
    public string? ProgramInput { get; set; }
    public required string LanguageName { get; set; }
}