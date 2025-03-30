namespace Codelabs.Core.DTOs;

public class UserDTO
{
    public int ID { get; set; }

    public string? Login { get; set; }

    public byte[]? Password { get; set; }
    
    public string? Name { get; set; }

    public string? Surname { get; set; }

    public RoleType Role { get; set; }

    public string? Phone { get; set; }

    public string? Email { get; set; } 

    public UserDTO? Author { get; set; }

    public List<LessonDTO>? Lessons { get; set; }
    
    public List<PurchaseDTO>? Purchases { get; set; }
}
