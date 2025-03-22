namespace Codelabs.Core.DTOs
{
    public class BuyerDTO
    {
        public int UserID { get; set; }

        public List<LessonDTO>? OwnedLessons { get; set; }
    }
}
