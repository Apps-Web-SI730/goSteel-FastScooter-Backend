namespace FastScooter.API.Response;

public class FavoriteResponse
{
    public int Id { get; init; }
    public required int UserId { get; init; }
    public required int ScooterId {get;init;}
}