using FastScooter.Infrastructure.Models;

namespace FastScooter.Domain.Interfaces;

public interface IFavoriteDomain
{
    public bool CreateNewFavorite(Favorites value);

    public bool RemoveFavorite(int id);
    Task<List<Favorites>> GetAllByUserId(int id);
    
    Task<List<Favorites>> GetAllByScooterId(int id);
    
    Task<bool> CancelFavorite(int id);

}