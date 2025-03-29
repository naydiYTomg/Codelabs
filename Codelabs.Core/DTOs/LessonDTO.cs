namespace Codelabs.Core.DTOs
{
    public class LessonDTO
    {
        public int ID { get; set; }

        public UserDTO Creator { get; set; }

        public List<BuyerDTO> Customers {  get; set; }
        
        public string? Name { get; set; }

        public string? Description { get; set; }

        //public FAILIK? File { get; set; } здесь файл нужно хранить

        public List<ExerciseDTO>? Exercises { get; set; }

        public bool isDeleted { get; set; }
    }
}
