using Microsoft.EntityFrameworkCore;

using FastScooter.Infrastructure.Context;
using FastScooter.Infrastructure.Dtos;
using FastScooter.Infrastructure.Interfaces;
using FastScooter.Infrastructure.Models;

namespace FastScooter.Infrastructure.Repositories;

public class UserMySQLInfrastructure : IUserInfrastructure
{
    // Dependency Injection
    private readonly FastScooterContext _fastScooterContext;
    
    // Constructor
    public UserMySQLInfrastructure(FastScooterContext fastScooterContext)
    {
        _fastScooterContext = fastScooterContext;
    }
    
    // Interface methods implementation
    public async Task<List<User>> GetUsersAsync()
    {
        return await _fastScooterContext.Users.Where(u => u.IsActive)
           .ToListAsync();
    }
    public async Task<User> GetUserByIdAsync(int id)
    {
        return await _fastScooterContext.Users.FirstOrDefaultAsync(u => u.Id == id) ?? throw new InvalidOperationException();
    }
    // ToDo [add to CreateUserAsync(User user)]:  BeginTransactionAsync(), CommitAsync() and RollbackAsync()
    public async Task<int> CreateUserAsync(User user)
    {
        user.IsActive = true;
        _fastScooterContext.Users.Add(user);
        await _fastScooterContext.SaveChangesAsync();
        
        return user.Id;
    }
    public async Task<bool> UpdateUserAsync(int id, UserDto value)
    {
        var user = await _fastScooterContext.Users.FirstOrDefaultAsync(u => u.Id == id);
        if (user == null) return false;
        
        user.Name = value.Name;
        user.Email = value.Email;
        user.Password = value.Password;
        user.DateUpdated = DateTime.Now;
        
        _fastScooterContext.Users.Update(user);
        await _fastScooterContext.SaveChangesAsync();
        
        return true;
    }
    public async Task<bool> DeleteUserAsync(int id)
    {
        var user = await _fastScooterContext.Users.FirstOrDefaultAsync(u => u.Id == id);
        if (user == null) return false;
        
        user.IsActive = false;
        _fastScooterContext.Users.Update(user);
        await _fastScooterContext.SaveChangesAsync();
        
        return true;
    }

    public bool ExistsByEmailAndPassword(string email, string password)
    {
        return _fastScooterContext.Users.Any(
            e => e.Email == email &&
                 e.Password == email &&
                 e.IsActive == true
        );
    }

    public int GetUserIdByEmailAndPassword(User user)
    {
        var matchingUser = _fastScooterContext.Users.FirstOrDefault(
            c => c.IsActive &&
                 c.Email == user.Email &&
                 c.Password == user.Password);
     
            return matchingUser.Id;
            
    }

    public int GetUserIdByEmail(User user)
    {
        var userToGet = _fastScooterContext.Users.FirstOrDefaultAsync(
            u => u.IsActive && u.Email == user.Email);
        return userToGet.Id;
    }

    // I'm not sure if the validations methods below should be placed in another Layer (maybe the API layer?)
    public bool ExistsByEmail(string email)
    {
        return _fastScooterContext.Users.Any(u => u.Email == email);
    }
    public bool ExistsByIdAndEmail(int id, string email)
    {
        return _fastScooterContext.Users.Any(u => u.Id == id && u.Email == email && u.IsActive);
    }
    public bool ExistsById(int id)
    {
        return _fastScooterContext.Users.Any(u => u.Id == id && u.IsActive);
    }

    public Task<int> Signup(User user)
    {
        throw new NotImplementedException();
    }
}