using Microsoft.EntityFrameworkCore;

using FastScooter.Infrastructure.Context;
using FastScooter.Infrastructure.Interfaces;
using FastScooter.Infrastructure.Models;

namespace FastScooter.Infrastructure.Repositories;

public class RentMySQLInfrastructure : IRentInfrastructure
{
    // Dependency Injection
    private readonly FastScooterContext _fastScooterContext;
    
    // Constructor
    public RentMySQLInfrastructure(FastScooterContext fastScooterContext)
    {
        _fastScooterContext = fastScooterContext;
    }
    
    // Interface methods implementation
    public async Task<List<Rent>> GetGetByScooterId(int scooterId)
    {
        return await _fastScooterContext.Rents.Where(r => r.ScooterId == scooterId).ToListAsync();
    }
    public async Task<int> CreateRentAsync(Rent rent)
    {
        _fastScooterContext.Rents.Add(rent);
        await _fastScooterContext.SaveChangesAsync();
        
        return rent.Id;
    }
}