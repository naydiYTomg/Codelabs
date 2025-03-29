using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Codelabs.Core.DTOs;

public class AuthorInfoDTO
{
    [Key, ForeignKey("User")]
    public int UserID { get; set; }
    public string? TIN { get; set; }
    public string? BankName { get; set; }
    public string? BIC { get; set; }
    public string? SettlementAccount { get; set; }
    public string? CorrespondentAccount { get; set; }

    public UserDTO? User { get; set; } = null!;

}