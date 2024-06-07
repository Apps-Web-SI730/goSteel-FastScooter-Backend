namespace FastScooter.Infrastructure.Models;

public class Rent : BaseModel
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public double Price { get; set; }
    // The following property is for relationship mapping (one to one)
    
    //fk Scooter
    public int ScooterId { get; set; }
    public virtual Scooter Scooter { get; set; }
    //fk User
    public virtual User User { get; set; } 
    public int UserId { get; set; }

    // ToDo: public Scooter Scooter { get; set; }
}