using FastScooter.Infrastructure.Models;
// using FastScooter.Infrastructure.Dtos;

namespace FastScooter.Domain.Interfaces;

public interface IUserDomain
{
    // ToDo:
    // Task<List<User>> GetAllUsersAsync();
    // User GetUserById (int id);
    public bool CreateUser(User user); // Save
    // ToDo:
    // public int UpdateUser(int id, UserDto value); // Update
    // public int DeleteUser(int id); // Delete
    // ToDo:
    // Task<User> GetByUsername(string username);
    // public int Login(User user);
    // Task<int> SignUp(User user);
}