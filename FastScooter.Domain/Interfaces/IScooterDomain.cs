using FastScooter.Infrastructure.Models;

namespace FastScooter.Domain.Interfaces;

public interface IScooterDomain
{
    // CUD: Create, Update and Delete
    Task<int> CreateScooterAsync(Scooter scooter);
    Task<bool> UpdateScooterAsync(int id, Scooter scooter);
    Task<bool> DeleteScooterAsync(int id);
    //
    // ToDo: Task<List<Scooter>> GetAvailableScootersAsync(DateTime start, DateTime end);
}