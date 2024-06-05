using FastScooter.Domain.Interfaces;
using FastScooter.Infrastructure.Interfaces;
using FastScooter.Infrastructure.Models;

namespace FastScooter.Domain.Domain;

public class FavoriteDomain:IFavoriteDomain
{

    private IFavoriteInfrastructure _favoriteInfrastructure;
    private IUserInfrastructure _userInfrastructure;
    private IScooterInfrastructure _scooterInfrastructure;


    public FavoriteDomain(IFavoriteInfrastructure favoriteInfrastructure, IUserInfrastructure userInfrastructure, IScooterInfrastructure scooterInfrastructure)
    {
        _favoriteInfrastructure = favoriteInfrastructure;
        _userInfrastructure = userInfrastructure;
        _scooterInfrastructure = scooterInfrastructure;
    }

    public bool Save(Favorites value)
    {
        return _favoriteInfrastructure.save(value);
    }
}