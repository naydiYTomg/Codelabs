namespace Codelabs.Core.OutputModels;

public class SolutionForExerciseCompetingOutputModel
{
    public int ID { get; set; }
    public int Attempts { get; set; }
    public string? CorrectAttemptPath { get; set; }
    public bool IsSolved { get; set; }
}