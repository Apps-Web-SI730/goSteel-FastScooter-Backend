using FastScooter.Infrastructure.Models;

namespace FastScooter.Domain.Interfaces;

public interface IFavoriteDomain
{
    public bool Save(Favorites value);

}