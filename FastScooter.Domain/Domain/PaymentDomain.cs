using FastScooter.Domain.Interfaces;
using FastScooter.Infrastructure.Interfaces;
using FastScooter.Infrastructure.Models;

namespace FastScooter.Domain.Domain;

public class PaymentDomain : IPaymentDomain
{
    private IPaymentInfrastructure _paymentInfrastructure;
    private IUserInfrastructure _userInfrastructure;
    private IRentInfrastructure _rentInfrastructure;


    public PaymentDomain(IPaymentInfrastructure paymentInfrastructure, IUserInfrastructure userInfrastructure, IRentInfrastructure rentInfrastructure)
    {
        _paymentInfrastructure = paymentInfrastructure;
        _userInfrastructure = userInfrastructure;
        _rentInfrastructure = rentInfrastructure;
    }


    public Task<List<Payment>> GetAllByUserId(int id)
    {
        if (!_userInfrastructure.ExistsById(id))
            throw new Exception("The user is invalid");
        return _paymentInfrastructure.GetByUserId(id);
    }

    public Task<List<Payment>> GetAllByRentId(int id)
    {
        if (!_rentInfrastructure.ExistsById(id))
            throw new Exception("The scooter is invalid");
        return _paymentInfrastructure.GetByRentId(id);
    }

    public bool CreateNewPayment(Payment value)
    {
        if (!_userInfrastructure.ExistsById(value.UserId))
            throw new Exception("The user is invalid");
        if (!_rentInfrastructure.ExistsById(value.RentId))
            throw new Exception("The scooter is invalid");

        return _paymentInfrastructure.save(value);
    }

    public bool RemovePayment(int id)
    {
        if (!_paymentInfrastructure.existsById(id))            
            throw new Exception("The favoirte is invalid");
        
        _paymentInfrastructure.delete(id);
        return true;
    }
    
    
}