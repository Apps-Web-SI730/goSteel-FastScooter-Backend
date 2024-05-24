using FastScooter.Infrastructure.Models;
using FastScooter.Infrastructure.Dtos;

namespace FastScooter.Domain.Interfaces;

public interface IUserDomain
{
    // CRUD
    Task<int> CreateUserAsync(User user);
    Task<bool> UpdateUserAsync(int id, UserDto value);
    Task<bool> DeleteUserAsync(int id);
    // 
    
}