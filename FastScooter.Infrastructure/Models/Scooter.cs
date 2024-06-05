namespace FastScooter.Infrastructure.Models;

public class Scooter : BaseModel
{
    public required string Name { get; set; }
    public required string Brand { get; set; }
    public required string Model { get; set; }
    public required string Description { get; set; }
    public required string ImageUrl { get; set; }
    public decimal Price { get; set; }
    
    public virtual ICollection<Rent> Rents { get; set; } // Colección de Rents


}