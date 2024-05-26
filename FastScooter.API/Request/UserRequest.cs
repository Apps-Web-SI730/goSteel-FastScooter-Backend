using System.ComponentModel.DataAnnotations;

namespace FastScooter.API.Request;

public class UserRequest
{
    [Required] [MaxLength(60)] public required string Name { get; set; }
    [Required] [MaxLength(60)] public required string Email { get; set; }
    [Required] [MinLength(5)] [MaxLength(100)]
    public required string Password { get; set; }
    [Required] [MaxLength(20)] public required string Role { get; set; }
    [Required] public DateTime BirthDate { get; set; }
    public List<ScooterRequest>? Scooters { get; set; }
}