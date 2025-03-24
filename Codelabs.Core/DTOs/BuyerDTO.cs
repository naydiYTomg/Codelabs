namespace Codelabs.Core.DTOs
{
    public class BuyerDTO
    {
        public int UserID { get; set; }

        public UserDTO User { get; set; }

        public List<LessonDTO>? OwnedLessons { get; set; }
    }
}
