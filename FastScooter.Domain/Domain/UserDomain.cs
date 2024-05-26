using FastScooter.Domain.Interfaces;
using FastScooter.Infrastructure.Dtos;
using FastScooter.Infrastructure.Interfaces;
using FastScooter.Infrastructure.Models;

namespace FastScooter.Domain.Domain;

public class UserDomain : IUserDomain
{
    // Dependency Injection
    private readonly IUserInfrastructure _userInfrastructure;
    
    // UserDomain Constructor
    public UserDomain(IUserInfrastructure userInfrastructure)
    {
        _userInfrastructure = userInfrastructure;
    }
    
    // Interface methods implementation
    // REMEMBER: Domain layer is responsible for business rules and NOT for data validation
    public async Task<int> CreateUserAsync(User user)
    {
        // TODO: Validations -> PASS TO API LAYER (Request and Response)
        if(ExistsByEmailValidation(user.Email)) throw new Exception("Email already exists");
        IsValidCreate(user);
        
        return await _userInfrastructure.CreateUserAsync(user);
    }
    public async Task<bool> UpdateUserAsync(int id, UserDto value)
    {
        // TODO: Validations -> PASS TO API LAYER (Request and Response)
        if (!ExistsByIdValidation(id)) throw new Exception("User doesn't exist");
        IsValidUpdate(value);
        if (!AllowedEmailUpdate(id, value)) throw new Exception("A user already exists with this email");
        
        return await _userInfrastructure.UpdateUserAsync(id, value);
    }
    public async Task<bool> DeleteUserAsync(int id)
    {
        // TODO: Validations -> PASS TO API LAYER (Request and Response)
        if (!ExistsByIdValidation(id)) throw new Exception("User doesn't exist");
        return await _userInfrastructure.DeleteUserAsync(id);
    }
    // TODO: Validations -> PASS TO API LAYER (Request and Response)
    private bool ExistsByEmailValidation(string email)
    {
        return _userInfrastructure.ExistsByEmail(email);
    }
    private static void IsValidCreate(User user)
    {
        if (string.IsNullOrEmpty(user.Name))throw new Exception("Name is required");
        if (string.IsNullOrEmpty(user.Email))throw new Exception("Email is required");
        if (string.IsNullOrEmpty(user.Password))throw new Exception("Password is required");
    }
    private bool ExistsByIdValidation(int id)
    {
        return _userInfrastructure.ExistsById(id);
    }
    private static void IsValidUpdate(UserDto user)
    {
        if (user.Name.Length > 50) throw new Exception("Name has to be less than 50 characters");
        if (user.Email.Length > 50) throw new Exception("Email has to be less than 50 characters");
    }
    private bool AllowedEmailUpdate(int id, UserDto user)
    {
        if (_userInfrastructure.ExistsByIdAndEmail(id, user.Email)) return true;
        if (ExistsByEmailValidation(user.Email)) throw new Exception("A user already exists with this email");
        return true;
    }
}