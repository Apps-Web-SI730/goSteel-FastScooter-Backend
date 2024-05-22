using FastScooter.Infrastructure.Context;
using FastScooter.Infrastructure.Interfaces;
using FastScooter.Infrastructure.Models;

namespace FastScooter.Infrastructure.Repositories;

public class UserMySQLInfrastructure : IUserInfrastructure
{
    // Dependecy Injection
    private readonly FastScooterContext _fastScooterContext;
    // Constructor
    public UserMySQLInfrastructure(FastScooterContext fastScooterContext)
    {
        _fastScooterContext = fastScooterContext;
    }
    // Interface methods implementation
    public bool CreateUser(User user)
    {
        user.DateCreated = DateTime.Now;
        _fastScooterContext.Users.Add(user);
        _fastScooterContext.SaveChanges();
        return true;
    }
    public bool ExistsByEmail(string email)
    {
        return _fastScooterContext.Users.Any(u => u.Email == email);
    }
}