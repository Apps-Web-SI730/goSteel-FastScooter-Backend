using FastScooter.Infrastructure.Models;

namespace FastScooter.Infrastructure.Interfaces;

public interface IScooterInfrastructure
{
    // CRUD: Create, Read, Update and Delete
    Task<List<Scooter>> GetScootersAsync();
    Task<Scooter> GetScooterByIdAsync(int id);
    Task<Scooter> GetScooterByUserIdAsync(int userId);
    Task<int> CreateScooterAsync(Scooter scooter);
    Task<bool> UpdateScooterAsync(int id, Scooter scooter);
    Task<bool> DeleteScooterAsync(int id);
    //
    public bool ExistsById(int id);
    
    List<Scooter> GetAll();
}