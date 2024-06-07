using FastScooter.Infrastructure.Models;

namespace FastScooter.Infrastructure.Interfaces;

public interface IPaymentInfrastructure
{
    public bool save(Payment payment);

    Task<List<Payment>> GetByUserId(int userId);
    
    Task<List<Payment>> GetByRentId(int rentId);
    
    public bool delete(int id);
    
    public bool existsById(int id);



}