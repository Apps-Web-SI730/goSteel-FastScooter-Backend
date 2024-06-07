namespace FastScooter.API.Response;

public class PaymentResponse
{
    public int Id { get; init; }
    public required int UserId { get; init; }
    public required int RentId { get; init; }

}