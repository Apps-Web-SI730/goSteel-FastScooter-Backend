using FastScooter.Domain.Interfaces;
using FastScooter.Infrastructure.Interfaces;
using FastScooter.Infrastructure.Models;

namespace FastScooter.Domain.Domain;

public class RentDomain : IRentDomain
{
    // Dependency Injection
    private readonly IRentInfrastructure _rentInfrastructure;
    private readonly IScooterInfrastructure _scooterInfrastructure;
    private readonly IUserInfrastructure _userInfrastructure;
    
    // RentDomain Constructor
    public RentDomain(IRentInfrastructure rentInfrastructure, IScooterInfrastructure scooterInfrastructure, IUserInfrastructure userInfrastructure)
    {
        _rentInfrastructure = rentInfrastructure;
        _scooterInfrastructure = scooterInfrastructure;
        _userInfrastructure = userInfrastructure;
    }
    
    // Interface methods implementation
    // REMEMBER: Domain layer is responsible for business rules and NOT for data validation
    public async Task<int> CreateRentAsync(Rent rent)
    {
        if (!_userInfrastructure.ExistsById(rent.UserId))
            throw new Exception("The user is invalid");
        if (!_scooterInfrastructure.ExistsById(rent.ScooterId))
            throw new Exception("The scooter is invalid");
        
        ValidateDate(rent.StartDate, rent.EndDate);

        return await _rentInfrastructure.CreateRentAsync(rent);
    }

    public bool AvailableScooter(int id, DateTime start, DateTime end)
    {
        try
        {
            ValidateScooterAvailability(id, start, end);
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }

    public List<Scooter> GetAvailableScooters(DateTime start, DateTime end)
    {
        List<Scooter> avaliableBScooters = new();
        List<Scooter> scooters = _scooterInfrastructure.GetAll();
        foreach (var scooter in scooters)
        {
            try
            {
                ValidateScooterAvailability(scooter.Id, start, end);
                avaliableBScooters.Add(scooter);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        return avaliableBScooters;
    }

    public bool CancelUnfinishedRent(int id)
    {
        if (!_rentInfrastructure.ExistsById(id))            
            throw new Exception("The Rent is invalid");
        
        _rentInfrastructure.delete(id);
        return true;
    }

    public Task<List<Rent>> GetAllByUserId(int id)
    {
        if (!_userInfrastructure.ExistsById(id))
            throw new Exception("The user is invalid");
        return _rentInfrastructure.GetByUserId(id);
    }

    private void ValidateScooterAvailability(int bikeId, DateTime startDate, DateTime endDate)
    {
        var rents = _rentInfrastructure.GetByScooterIdNoAsync(bikeId);
        foreach (var rent in rents)
        {
            if (rent.StartDate <= startDate && startDate <= rent.EndDate)
                throw new Exception("The bike is not available in the selected date");
            if (rent.StartDate <= endDate && endDate <= rent.EndDate)
                throw new Exception("The bike is not available in the selected date");
            if (startDate <= rent.StartDate && rent.StartDate <= endDate)
                throw new Exception("The bike is not available in the selected date");
            if (startDate <= rent.EndDate && rent.EndDate <= endDate)
                throw new Exception("The bike is not available in the selected date");
        }
    }
    private void ValidateDate(DateTime StartDate, DateTime EndDate)
    {
        var tomorrow = DateTime.Now.Date.AddDays(1);
        if (StartDate > EndDate) throw new Exception("The start date must be before the end date");
        if (StartDate == EndDate) throw new Exception("The start date must be different from the end date");

    }
}