using FastScooter.Infrastructure.Models;
using FastScooter.Infrastructure.Dtos;

namespace FastScooter.Domain.Interfaces;

public interface IUserDomain
{
    // CUD: Create, Update and Delete
    Task<int> CreateUserAsync(User user);
    Task<bool> UpdateUserAsync(int id, UserDto value);
    Task<bool> DeleteUserAsync(int id);
    // 
    public int Login(User user);

    
}
