using System.ComponentModel.DataAnnotations;

namespace Codelabs.Core.InputModels
{
    public class LessonInputModel
    {
        [Required(ErrorMessage = "Введите название")]
        [StringLength(100, 
            MinimumLength = 6, 
            ErrorMessage = "Длина названия должна быть менее 100 символов и более 6 символов")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Введите краткое описание")]
        [StringLength(200, 
            MinimumLength = 6, 
            ErrorMessage = "Длина названия должна быть менее 200 символов и более 6 символов")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Введите цену")]
        public decimal? Cost { get; set; } = null;

        [Required(ErrorMessage = "Выберите язык программирования")]
        public int? LanguageID { get; set; }

        public int? AuthorID { get; set; }

        [Required(ErrorMessage = "Введите контент урока")]
        public string? Content { get; set; }

        public List<ExerciseInputModel> Exercises { get; set; } = new();
    }
}