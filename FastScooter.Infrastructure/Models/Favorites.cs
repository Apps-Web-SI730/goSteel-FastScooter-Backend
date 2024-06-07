namespace FastScooter.Infrastructure.Models;

public class Favorites :BaseModel
{
    //fk Scooter
    public int ScooterId { get; set; }
    public virtual Scooter Scooter { get; set; }
    //fk User
    public virtual User User { get; set; } 
    public int UserId { get; set; }

}