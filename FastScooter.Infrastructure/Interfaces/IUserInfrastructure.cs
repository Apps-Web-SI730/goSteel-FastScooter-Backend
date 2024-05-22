using FastScooter.Infrastructure.Models;

namespace FastScooter.Infrastructure.Interfaces;

public interface IUserInfrastructure
{
    // ToDo
    // Task<List<User>> GetAllAsync();
    public bool CreateUser(User user);
    public bool ExistsByEmail(string email);
}