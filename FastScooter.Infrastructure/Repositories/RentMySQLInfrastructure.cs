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
    public  List<Rent> GetByScooterIdNoAsync(int scooterId)
    {
        return  _fastScooterContext.Rents.Where(r => r.ScooterId == scooterId).ToList();
    }
    public async Task<int> CreateRentAsync(Rent rent)
    {
        rent.IsActive = true;
        _fastScooterContext.Rents.Add(rent);
        await _fastScooterContext.SaveChangesAsync();
        
        return rent.Id;
    }

    public async Task<List<Rent>> GetByUserId(int userId)
    {
        return await _fastScooterContext.Rents.Where(rent => rent.UserId == userId).ToListAsync();
    }

    public async Task<List<Rent>> GetByScooterId(int scooterId)
    {
        return await _fastScooterContext.Rents.Where(rent => rent.UserId == scooterId).ToListAsync();
    }

    public async Task<List<Rent>> GetAll()
    {
        return await _fastScooterContext.Rents.Where(x=>x.IsActive==true).ToListAsync();
    }

    public bool delete(int id)
    {
        var user = _fastScooterContext.Payments.Find(id);
        user.IsActive = false;
        _fastScooterContext.Payments.Update(user);
        _fastScooterContext.SaveChanges();
        return true;
    }

    public bool ExistsById(int id)
    {
        return _fastScooterContext.Rents.Any(rent=>rent.ScooterId==id);
    }
}