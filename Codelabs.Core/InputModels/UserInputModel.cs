using System.ComponentModel.DataAnnotations;

namespace Codelabs.Core.InputModels
{
    public class UserInputModel
    {
        public int? ID { get; set; }

        [Required(ErrorMessage = "Введите логин")]
        [StringLength(16, 
            MinimumLength = 2, 
            ErrorMessage = "Длина логина должна быть более 2 символов и менее 16 символов")]
        public string? Login { get; set; }

        [Required(ErrorMessage = "Введите пароль")]
        [StringLength(32, 
            MinimumLength = 6,
            ErrorMessage = "Длина пароля должна быть более 6 символов и менее 32 символов")]
        public string? Password { get; set; }

        public string? Name { get; set; }

        public string? Surname { get; set; }

        public RoleType Role { get; set; }

        [MinLength(11, ErrorMessage = "Длина телефона не может быть меньше 11 символов")]
        public string? Phone { get; set; }

        public string? Email { get; set; }

        [StringLength(12, 
            MinimumLength = 12,
            ErrorMessage = "Длина ИНН должна равняться 12 символам")]
        public string? TIN { get; set; }
    }
}