using Codelabs.Core.DTOs;

namespace Codelabs.Core.OutputModels;

public class LessonForShowcaseOutputModel
{
    public int ID { get; set; }

    public required UserDTO Author { get; set; }

    public required string Name { get; set; }

    public required string Description { get; set; }

    public decimal Cost { get; set; }

    public required LanguageDTO Language { get; set; }

    public DateTime? CreatedTime { get; set; }
}