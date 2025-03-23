namespace Codelabs.Core.DTOs
{
    public class BuyerDTO
    {
        public int ID { get; set; }

        public int UserID { get; set; }

        public List<LessonDTO>? OwnedLessons { get; set; }
    }
}
