namespace Codelabs.Core.DTOs;

public class LanguageDTO
{
    public int ID { get; set; }

    public string Name { get; set; }

    public List<LessonDTO> Lessons { get; set; }
}