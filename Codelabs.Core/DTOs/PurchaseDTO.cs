namespace Codelabs.Core.DTOs;

public class PurchaseDTO
{
    public int ID { get; set; }

    public bool IsVisited { get; set; }

    public UserDTO User { get; set; }

    public LessonDTO Lesson { get; set; }
    public List<SolutionDTO>? Solutions { get; set; }
    public DateTime Date { get; set; }
}