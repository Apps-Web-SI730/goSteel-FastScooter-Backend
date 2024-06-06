namespace FastScooter.Infrastructure.Models;

public class Payment : BaseModel
{
    public double Amount { get; set; }
    
    public DateTime PaymentDate { get; set; }
    
    //fk User
    public virtual User User { get; set; } 
    public int UserId { get; set; }
    
    //fk Rent
    public virtual Rent Rent { get; set; } 
    public int RentId { get; set; }

    public string PaymentMethod { get; set; }
    
    
}