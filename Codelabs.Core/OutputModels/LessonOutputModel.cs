namespace Codelabs.Core.OutputModels
{
    public class LessonOutputModel
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal? Cost { get; set; }

        public string? Content { get; set; }

        public LanguageOutputModel? Language { get; set; }

        public UserOutputModel? Author { get; set; }

        public List<ExerciseOutputModel> Exercises { get; set; } = new();
    }
}