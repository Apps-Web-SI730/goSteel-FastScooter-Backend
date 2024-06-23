using FastScooter.Domain.IAM.Models;

namespace FastScooter.Domain.IAM.Repositories;

public interface IUserRepositories
{
    Task<int> RegisterUserAsync(User user);
    
}