namespace Codelabs.Core.DTOs
{
    public class SellerDTO
    {
        public int ID { get; set; }

        public int UserID { get; set; }

        public string? Name { get; set; }

        public string? Surname { get; set; }

        public List<LessonDTO>? CreatedLessons { get; set; }
    }
}
