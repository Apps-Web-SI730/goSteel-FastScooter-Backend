using FastScooter.Infrastructure.Models;

namespace FastScooter.Infrastructure.Interfaces;

public interface IFavoriteInfrastructure
{
    public bool save(Favorites favorites);
    
}