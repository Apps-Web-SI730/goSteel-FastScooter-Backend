using FastScooter.Domain.Interfaces;
using FastScooter.Infrastructure.Interfaces;
using FastScooter.Infrastructure.Models;

namespace FastScooter.Domain.Domain;

public class RentDomain : IRentDomain
{
    // Dependency Injection
    private readonly IRentInfrastructure _rentInfrastructure;
    
    // RentDomain Constructor
    public RentDomain(IRentInfrastructure rentInfrastructure)
    {
        _rentInfrastructure = rentInfrastructure;
    }
    
    // Interface methods implementation
    // REMEMBER: Domain layer is responsible for business rules and NOT for data validation
    public async Task<int> CreateRentAsync(Rent rent)
    {
        // TODO: Validations -> PASS TO API LAYER (Request and Response)
        // ValidateRent(rent);
        
        return await _rentInfrastructure.CreateRentAsync(rent);
    }
    // TODO: Validations -> PASS TO API LAYER (Request and Response)
    // private void ValidateRent(Rent rent)
    // {
    //     
    // }
}