namespace FastScooter.Infrastructure.Models;

public class User : BaseModel
{
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    // ToDo:
    // public string Role { get; set; }
    // public DateTime BirthDate { get; set; }
    // public virtual List<Scooters> Scooters { get; set; }
}