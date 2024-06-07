using FastScooter.Infrastructure.Models;

namespace FastScooter.Domain.Interfaces;

public interface IPaymentDomain
{
    Task<List<Payment>> GetAllByUserId(int id);
    
    Task<List<Payment>> GetAllByRentId(int id);
    
    public bool CreateNewPayment(Payment value);
    
    public bool RemovePayment(int id);


    

}