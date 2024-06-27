using FastScooter.Domain.Model;
using FastScooter.Infrastructure.Models;
using FastScooter.Infrastructure.Dtos;

namespace FastScooter.Domain.Interfaces;

public interface IUserDomain
{
    // CUD: Create, Update and Delete
    Task<int> CreateUserAsync(User user);
    Task<bool> UpdateUserAsync(int id, UserDto value);
    Task<bool> DeleteUserAsync(int id);
    
    // Auth
    Task<User> GetByUsername(string username);
    Task<LoginResponse> LoginRev1(User user);
    Task<int> SignupRev1(User user);
    
    
  
    public int Login(User user);

    
}
