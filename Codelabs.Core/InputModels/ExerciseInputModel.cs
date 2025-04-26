using System.ComponentModel.DataAnnotations;

namespace Codelabs.Core.InputModels
{
    public class ExerciseInputModel
    {
        public int? ID { get; set; }

        [Required(ErrorMessage = "Введите название")]
        [StringLength(12, 
            MinimumLength = 3, 
            ErrorMessage = "Длина названия должна быть менее 12 символов и более 3 символов")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Введите ожидаемый вывод")]
        public string DesiredOutput { get; set; }

        public string? ProgramInput { get; set; }

        [Required(ErrorMessage = "Введите требования")]
        public string Requirements { get; set; }
    }
}