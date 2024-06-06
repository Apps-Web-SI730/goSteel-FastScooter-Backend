using FastScooter.Infrastructure.Models;

namespace FastScooter.Infrastructure.Interfaces;

public interface IRentInfrastructure
{
    // CRUD: Read and Create
    List<Rent> GetByScooterIdNoAsync(int scooterId); 
    Task<int> CreateRentAsync(Rent rent);
    Task<List<Rent>> GetByUserId(int userId);
    Task<List<Rent>> GetByScooterId(int scooterId);
    List<Rent> GetAll();
    public bool delete(int id);
    
    public bool ExistsById(int id);


}