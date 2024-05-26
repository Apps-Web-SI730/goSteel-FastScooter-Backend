namespace FastScooter.Infrastructure.Models;

public class User : BaseModel
{
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public required string Role { get; set; }
    public DateTime BirthDate { get; set; }
    // The following property is for relationship mapping (one-to-many)
    public List<Scooter>? Scooters { get; set; }
}