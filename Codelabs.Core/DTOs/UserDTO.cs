namespace Codelabs.Core.DTOs
{
    public class UserDTO
    {
        public int Id { get; set; }

        public string? Login { get; set; }

        public byte[]? Password { get; set; }
        
        public string? Name { get; set; }

        public string? Surname { get; set; }

        public Role Role { get; set; }

        public string? Phone { get; set; }

        public string? Email { get; set; }
    }
}
