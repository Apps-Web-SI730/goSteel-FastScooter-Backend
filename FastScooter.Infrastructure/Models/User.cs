namespace FastScooter.Infrastructure.Models;

public class User : BaseModel
{
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public required string Role { get; set; }
    public DateTime BirthDate { get; set; }
    public virtual ICollection<Rent> Rents { get; set; } // Colección de Rents
    public virtual ICollection<Favorites> Favorites { get; set; } // Colección de Rents

    // The following property is for relationship mapping (one-to-many)
}