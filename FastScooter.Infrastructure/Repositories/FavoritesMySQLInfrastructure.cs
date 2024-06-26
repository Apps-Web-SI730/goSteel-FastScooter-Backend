using FastScooter.Infrastructure.Context;
using FastScooter.Infrastructure.Interfaces;
using FastScooter.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace FastScooter.Infrastructure.Repositories;

public class FavoritesMySQLInfrastructure : IFavoriteInfrastructure
{
    private readonly FastScooterContext _fastScooterContext;

    public FavoritesMySQLInfrastructure(FastScooterContext fastScooterContext)
    {
        _fastScooterContext = fastScooterContext;
    }
    public bool save(Favorites favorites)
    {           favorites.IsActive = true;
        _fastScooterContext.Favorites.Add(favorites);
        _fastScooterContext.SaveChanges();
        return true;
    }

    public async Task<bool> DeleteUserAsync(int id)
    {
        var user = await _fastScooterContext.Favorites.FirstOrDefaultAsync(u => u.Id == id);
        if (user == null) return false;
        
        user.IsActive = false;
        _fastScooterContext.Favorites.Update(user);
        await _fastScooterContext.SaveChangesAsync();
        
        return true;
    }

    public bool delete(int id)
    {
        var user = _fastScooterContext.Favorites.Find(id);
        user.IsActive = false;
        _fastScooterContext.Favorites.Update(user);
        _fastScooterContext.SaveChanges();
        return true;
    }

    public async Task<List<Favorites>> GetByUserId(int userId)
    {
        return await _fastScooterContext.Favorites.Where(favorites => favorites.UserId == userId).ToListAsync();
        

    }

    public bool existsById(int id)
    {
        return _fastScooterContext.Favorites.Any(favorite => favorite.Id == id && favorite.IsActive == true);
    }
}