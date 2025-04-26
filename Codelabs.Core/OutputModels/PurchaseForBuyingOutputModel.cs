using Codelabs.Core.DTOs;

namespace Codelabs.Core.OutputModels;

public class PurchaseForBuyingOutputModel
{
    public required UserDTO User { get; set; }

    public required LessonDTO Lesson { get; set; }
}