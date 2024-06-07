using FastScooter.Infrastructure.Models;

namespace FastScooter.Infrastructure.Interfaces;

public interface IFavoriteInfrastructure
{
    public bool save(Favorites favorites);
    Task<bool> DeleteUserAsync(int id);
    Task<List<Favorites>> GetByUserId(int userId);

    public bool existsById(int id);
}