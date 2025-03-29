namespace Codelabs.Core.DTOs;

public class SolutionDTO
{
    public int ID { get; set; }
    public ExerciseDTO Exercise { get; set; }
    public PurchaseDto Purchase { get; set; }
    public int Attempts { get; set; }
    public string? CorrectAttempt { get; set; }
    public bool IsSolved { get; set; }
}