namespace FastScooter.API.Response;

public class UserResponse
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
    // Remenber: If you modify the User.cs (from FastScooter.Infrastructure.Models) class, you should modify this class too.
}