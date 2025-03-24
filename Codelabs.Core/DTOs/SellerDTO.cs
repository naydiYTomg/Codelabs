namespace Codelabs.Core.DTOs
{
    public class SellerDTO
    {
        public int UserID { get; set; }

        public UserDTO User { get; set; }

        public string? Name { get; set; }

        public string? Surname { get; set; }

        public List<LessonDTO>? CreatedLessons { get; set; }
    }
}
