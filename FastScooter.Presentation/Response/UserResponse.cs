namespace FastScooter.API.Response;

public class UserResponse
{
    public int Id { get; init; }
    public required string Name { get; init; }
    public required string Email { get; init; }
    // Remenber: If you modify the User.cs (from FastScooter.Infrastructure.Models) class, you should modify this class too.
}