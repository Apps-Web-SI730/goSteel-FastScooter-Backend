using FastScooter.Infrastructure.Models;

namespace FastScooter.Domain.Interfaces;

public interface IRentDomain
{
    // CRUD: Create
    Task<int> CreateRentAsync(Rent rent);
}