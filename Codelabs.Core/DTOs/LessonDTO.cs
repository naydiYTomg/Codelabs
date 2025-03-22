namespace Codelabs.Core.DTOs
{
    public class LessonDTO
    {
        public int ID { get; set; }

        public int CourseID { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public List<ExerciseDTO>? Exercises { get; set; }
    }
}
