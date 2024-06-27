using FastScooter.Infrastructure.Models;

namespace FastScooter.Domain.Interfaces;

public interface IRentDomain
{
    // CRUD: Create
    Task<int> CreateRentAsync(Rent rent);
    
    public bool AvailableScooter(int id, DateTime start, DateTime end);
    
    public List<Scooter> GetAvailableScooters(DateTime start, DateTime end);
    
    public bool CancelUnfinishedRent(int id);

    Task<List<Rent>> GetAllByUserId(int id);


}