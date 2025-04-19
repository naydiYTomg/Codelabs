using Codelabs.Core.DTOs;

namespace Codelabs.Core.OutputModels;

public class PurchaseForBuyingOutputModel
{
    // public int ID { get; set; }

    // public bool IsVisited { get; set; }

    public required UserDTO User { get; set; }

    public required LessonDTO Lesson { get; set; }
    // public List<SolutionDTO>? Solutions { get; set; }
    // public DateTime Date { get; set; }
}