namespace Codelabs.Core.DTOs;

public class LessonDTO
{
    public int ID { get; set; }

    public UserDTO Author { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public string ContentPath { get; set; }

    public bool IsDeleted { get; set; }

    public LanguageDTO Language { get; set; } 
    public List<PurchaseDTO>? Purchases { get; set; }
    public List<ExerciseDTO>? Exercises { get; set; }
}
