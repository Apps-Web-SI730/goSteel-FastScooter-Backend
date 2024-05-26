using FastScooter.Infrastructure.Models;

namespace FastScooter.Infrastructure.Interfaces;

public interface IRentInfrastructure
{
    // CRUD: Read and Create
    Task<List<Rent>> GetGetByScooterId(int scooterId); 
    Task<int> CreateRentAsync(Rent rent);
}