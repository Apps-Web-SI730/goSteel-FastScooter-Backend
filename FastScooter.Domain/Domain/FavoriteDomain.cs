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

    public bool CreateNewFavorite(Favorites value)
    {
        if (!_userInfrastructure.ExistsById(value.UserId))
            throw new Exception("The user is invalid");
        if (!_scooterInfrastructure.ExistsById(value.ScooterId))
            throw new Exception("The scooter is invalid");

        return _favoriteInfrastructure.save(value);
    }

    public bool RemoveFavorite(int id)
    {
        if (!_favoriteInfrastructure.existsById(id))            
            throw new Exception("The favoirte is invalid");
        
        _favoriteInfrastructure.DeleteUserAsync(id);
        return true;

    }

    public Task<List<Favorites>> GetAllByUserId(int id)
    {
        if (!_userInfrastructure.ExistsById(id))
            throw new Exception("The user is invalid");
        return _favoriteInfrastructure.GetByUserId(id);
    }

    public Task<List<Favorites>> GetAllByScooterId(int id)
    {
        if (!_userInfrastructure.ExistsById(id))
            throw new Exception("The Scooter is invalid");
        return _favoriteInfrastructure.GetByUserId(id);    }

    public async Task<bool>  CancelFavorite(int id)
    {
        if (!_favoriteInfrastructure.existsById(id))            
            throw new Exception("The Rent is invalid");
        
        return await  _favoriteInfrastructure.DeleteUserAsync(id);
   
    }
}