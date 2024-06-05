using FastScooter.Infrastructure.Context;
using FastScooter.Infrastructure.Interfaces;
using FastScooter.Infrastructure.Models;

namespace FastScooter.Infrastructure.Repositories;

public class FavoritesMySQLInfrastructure : IFavoriteInfrastructure
{
    private readonly FastScooterContext _fastScooterContext;

    public FavoritesMySQLInfrastructure(FastScooterContext fastScooterContext)
    {
        _fastScooterContext = fastScooterContext;
    }
    public bool save(Favorites favorites)
    {
        _fastScooterContext.Favorites.Add(favorites);
        _fastScooterContext.SaveChanges();
        return true;
    }
}