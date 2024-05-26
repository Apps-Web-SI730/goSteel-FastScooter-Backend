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
        return await _fastScooterContext.Users.Where(u => u.IsActive).ToListAsync();
    }
    public async Task<User> GetUserByIdAsync(int id)
    {
        return await _fastScooterContext.Users.FirstOrDefaultAsync(u => u.Id == id) ?? throw new InvalidOperationException();
    }
    // ToDo [add to CreateUserAsync(User user)]:  BeginTransactionAsync(), CommitAsync() and RollbackAsync()
    public async Task<int> CreateUserAsync(User user)
    {
        user.IsActive = true;
        // user.DateCreated = DateTime.Now;
        
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
}