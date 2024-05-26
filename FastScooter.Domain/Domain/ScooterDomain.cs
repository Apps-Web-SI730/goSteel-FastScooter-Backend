using FastScooter.Domain.Interfaces;
using FastScooter.Infrastructure.Interfaces;
using FastScooter.Infrastructure.Models;

namespace FastScooter.Domain.Domain;

public class ScooterDomain : IScooterDomain
{
    // Dependency Injection
    private readonly IScooterInfrastructure _scooterInfrastructure;
    
    // ScooterDomain Constructor
    public ScooterDomain(IScooterInfrastructure scooterInfrastructure)
    {
        _scooterInfrastructure = scooterInfrastructure;
    }
    
    // Interface methods implementation
    // REMEMBER: Domain layer is responsible for business rules and NOT for data validation
    public async Task<int> CreateScooterAsync(Scooter scooter)
    {
        // TODO: Validations -> PASS TO API LAYER (Request and Response)
        IsValidCreate(scooter);
        ExistsByIdValidation(scooter.UserId);
        
        return await _scooterInfrastructure.CreateScooterAsync(scooter);
    }
    public async Task<bool> UpdateScooterAsync(int id, Scooter scooter)
    {
        // TODO: Validations -> PASS TO API LAYER (Request and Response)
        if(!ExistsByIdValidation(id)) throw new Exception("Scooter doesn't exist");
        
        return await _scooterInfrastructure.UpdateScooterAsync(id, scooter);
    }
    public async Task<bool> DeleteScooterAsync(int id)
    {
        // TODO: Validations -> PASS TO API LAYER (Request and Response)
        if (!ExistsByIdValidation(id)) throw new Exception("Scooter doesn't exist");
        return await _scooterInfrastructure.DeleteScooterAsync(id);
    }
    // TODO: Validations -> PASS TO API LAYER (Request and Response)
    private static void IsValidCreate(Scooter scooter)
    {
        if (string.IsNullOrEmpty(scooter.Name)) throw new Exception("Name is required");
        if (scooter.Name.Length > 50) throw new Exception("Name has to be less than 50 characters");
        if (string.IsNullOrEmpty(scooter.Brand)) throw new Exception("Brand is required");
        if (scooter.Brand.Length > 50) throw new Exception("Brand has to be less than 50 characters");
        if (string.IsNullOrEmpty(scooter.Model)) throw new Exception("Model is required");
        if (scooter.Model.Length > 50) throw new Exception("Model has to be less than 50 characters");
        if (string.IsNullOrEmpty(scooter.Description)) throw new Exception("Description is required");
        if (scooter.Description.Length > 500) throw new Exception("Description has to be less than 500 characters");
        if (scooter.Price <= 0) throw new Exception("Price has to be greater than 0");
        if (scooter.UserId <= 0) throw new Exception("UserId has to be greater than 0");
    }
    private bool ExistsByIdValidation(int id)
    {
        return _scooterInfrastructure.ExistsById(id);
    }
}