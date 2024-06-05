using FastScooter.Infrastructure.Dtos;
using FastScooter.Infrastructure.Models;

namespace FastScooter.Infrastructure.Interfaces;

public interface IUserInfrastructure
{
    // CRUD: Create, Read, Update and Delete
    Task<List<User>> GetUsersAsync();
    Task<User> GetUserByIdAsync(int id);
    Task<int> CreateUserAsync(User user);
    Task<bool> UpdateUserAsync(int id, UserDto value);
    Task<bool> DeleteUserAsync(int id);
    public bool ExistsByEmailAndPassword(string email, string password);
    public int GetUserIdByEmailAndPassword(User user);
    public int GetUserIdByEmail(User user);


    // exists
    public bool ExistsByEmail(string email);
    public bool ExistsByIdAndEmail(int id, string email);
    public bool ExistsById(int id);
    Task<int> Signup(User user);

}