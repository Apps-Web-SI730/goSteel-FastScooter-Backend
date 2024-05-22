using FastScooter.Domain.Interfaces;
// using FastScooter.Infrastructure.Dtos;
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
    // ToDO:
    // public async Task<List<User>> GetAllUsersAsync()
    // {
    //     return await _userInfrastructure.GetAllAsync();
    // }
    // ToDO:
    // public User GetUserById(int id)
    // {
    //     
    // }
    
    public bool CreateUser(User user)
    {
        if(ExistsByEmailValidation(user.Email)) throw new Exception("Email already exists");
        IsValidCreate(user);
        return _userInfrastructure.CreateUser(user);
    }
    
    // ToDO:
    // public int UpdateUser(int id, UserDto userDto)
    // {
    //     
    // }
    // ToDO: 
    // public int DeleteUser(int id)
    // {
    //     
    // }
    
    // TODO: Validations (PASS TO API LAYER)
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
}