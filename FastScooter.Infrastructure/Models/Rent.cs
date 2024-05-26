namespace FastScooter.Infrastructure.Models;

public class Rent : BaseModel
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public double Price { get; set; }
    // The following property is for relationship mapping (one to one)
    public int ScooterId { get; set; }
    // ToDo: public Scooter Scooter { get; set; }
}