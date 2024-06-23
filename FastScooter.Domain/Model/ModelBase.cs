namespace FastScooter.Domain.Model;

public class ModelBase
{
    public int Id { get; init; }
    public DateTime CreatedAt { get; init; } = DateTime.Now;
    public DateTime UpdatedAt { get; init; }
    public bool IsActive { get; init; }
    public int CreatedUser { get; init; }
    public int? UpdatedUser { get; init; }
    
}