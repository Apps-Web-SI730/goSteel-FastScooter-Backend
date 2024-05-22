using System.ComponentModel.DataAnnotations;

namespace FastScooter.API.Request;

public class UserRequest
{
    [Required] [MaxLength(60)] public required string Name { get; set; }
    [Required] [MaxLength(60)] public required string Email { get; set; }
    [Required] [MinLength(5)] [MaxLength(100)]
    public required string Password { get; set; }
    // Remenber: If you modify the User.cs (from FastScooter.Infrastructure.Models) class, you should modify this class too.
}