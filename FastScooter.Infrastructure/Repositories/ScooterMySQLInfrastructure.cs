using Microsoft.EntityFrameworkCore;

using FastScooter.Infrastructure.Context;
using FastScooter.Infrastructure.Interfaces;
using FastScooter.Infrastructure.Models;

namespace FastScooter.Infrastructure.Repositories;

public class ScooterMySQLInfrastructure : IScooterInfrastructure
{
    // Dependency Injection
    private readonly FastScooterContext _fastScooterContext;
    
    // Constructor
    public ScooterMySQLInfrastructure(FastScooterContext fastScooterContext)
    {
        _fastScooterContext = fastScooterContext;
    }
    
    // Interface methods implementation
    public async Task<List<Scooter>> GetScootersAsync()
    {
        return await _fastScooterContext.Scooters.Where(s => s.IsActive).ToListAsync();
    }
    public async Task<Scooter> GetScooterByIdAsync(int id)
    {
        return await _fastScooterContext.Scooters.FirstOrDefaultAsync(s => s.Id == id) ?? throw new InvalidOperationException();
    }
    public async Task<Scooter> GetScooterByUserIdAsync(int userId)
    {
        return await _fastScooterContext.Scooters.FirstOrDefaultAsync(s => s.UserId == userId) ?? throw new InvalidOperationException();
    }
    public async Task<int> CreateScooterAsync(Scooter scooter)
    {
        scooter.IsActive = true;
        // scooter.DateCreated = DateTime.Now;
        
        _fastScooterContext.Scooters.Add(scooter);
        await _fastScooterContext.SaveChangesAsync();
        
        return scooter.Id;
    }
    // Maybe: consider using a DTO instead of the model for Scooter value
    public async Task<bool> UpdateScooterAsync(int id, Scooter value)
    {
        value.Id = id;
        value.DateUpdated = DateTime.Now;
        
        _fastScooterContext.Scooters.Update(value);
        await _fastScooterContext.SaveChangesAsync();
        
        return true;
    }
    public async Task<bool> DeleteScooterAsync(int id)
    {
        var scooter = await _fastScooterContext.Scooters.FirstOrDefaultAsync(s => s.Id == id);
        if (scooter == null) return false;
        
        scooter.IsActive = false;
        
        _fastScooterContext.Scooters.Update(scooter);
        await _fastScooterContext.SaveChangesAsync();
        
        return true;
    }
    // 
    public bool ExistsById(int id)
    {
        return _fastScooterContext.Scooters.Any(s => s.Id == id && s.IsActive);
    }
}