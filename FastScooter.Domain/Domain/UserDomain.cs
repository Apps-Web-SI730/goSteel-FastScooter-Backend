using FastScooter.Domain.Interfaces;
using FastScooter.Domain.Model;
using FastScooter.Infrastructure.Dtos;
using FastScooter.Infrastructure.Interfaces;
using FastScooter.Infrastructure.Models;

namespace FastScooter.Domain.Domain;

public class UserDomain : IUserDomain
{
    // Dependency Injection
    private readonly IUserInfrastructure _userInfrastructure;
    private IEncryptDomain _encryptDomain;
    private ITokenDomain _tokenDomain;
    // UserDomain Constructor
    public UserDomain(IUserInfrastructure userInfrastructure, ITokenDomain tokenDomain, IEncryptDomain encryptDomain)
    {
        _userInfrastructure = userInfrastructure;
        _tokenDomain = tokenDomain;
        _encryptDomain = encryptDomain;
    }
    
    // Interface methods implementation
    // REMEMBER: Domain layer is responsible for business rules and NOT for data validation
    public async Task<int> CreateUserAsync(User user)
    {
        // TODO: Validations -> PASS TO API LAYER (Request and Response)
        if(ExistsByEmailValidation(user.Email)) throw new Exception("Email already exists");
        IsValidCreate(user);
        
        return await _userInfrastructure.CreateUserAsync(user);
    }
    public async Task<bool> UpdateUserAsync(int id, UserDto value)
    {
        // TODO: Validations -> PASS TO API LAYER (Request and Response)
        if (!ExistsByIdValidation(id)) throw new Exception("User doesn't exist");
        IsValidUpdate(value);
        if (!AllowedEmailUpdate(id, value)) throw new Exception("A user already exists with this email");
        
        return await _userInfrastructure.UpdateUserAsync(id, value);
    }
    public async Task<bool> DeleteUserAsync(int id)
    {
        // TODO: Validations -> PASS TO API LAYER (Request and Response)
        if (!ExistsByIdValidation(id)) throw new Exception("User doesn't exist");
        return await _userInfrastructure.DeleteUserAsync(id);
    }

    public async Task<User> GetByUsername(string username)
    {
        return await _userInfrastructure.GetByUsername(username);
    }

    public async Task<LoginResponse> LoginRev1(User user)
    {
        var foundUser = await _userInfrastructure.GetByUsername(user.Email);
        
        if (_encryptDomain.Encrypt(user.Password) == foundUser.Password)
        {
            var token = _tokenDomain.GenerateJwt(foundUser.Email);
            var userId = foundUser.Id;
            return new LoginResponse
            {
                Token = token,
                Id = userId
            };
        }
        
        throw new ArgumentException("Invalid username or password");
    }

    public async Task<int> SignupRev1(User user)
    {
        if (ExistsByEmailValidation(user.Email)) throw new Exception("A user already exists with this email");
        IsValidSave(user);
        user.Password = _encryptDomain.Encrypt(user.Password);
        return await _userInfrastructure.Signup(user);
    }
    private static void IsValidSave(User user)
    {
        if (user.Name.Length == 0) throw new Exception("Name is required");
        if (user.Email.Length == 0) throw new Exception("Email is required");
        if (user.Password.Length == 0) throw new Exception("Password is required");
        if (user.BirthDate.ToString().Length == 0) throw new Exception("BirthDate is required");
        if (user.Name.Length > 50) throw new Exception("Name has to be less than 50 characters");
        if (user.Email.Length > 50) throw new Exception("Email has to be less than 50 characters");
        if (user.BirthDate > DateTime.Now.AddYears(-15)) throw new Exception("User has to be at least 15 years old");
    }

    public int Login(User user)
    {
        try
        {
            if (_userInfrastructure.ExistsByEmailAndPassword(user.Email, user.Password))
                return _userInfrastructure.GetUserIdByEmailAndPassword(user);
        }
        catch (Exception e)
        {
            throw new Exception("No se pudo realizar el pedido");
        }

        throw new Exception("El usuario o la contraseña esta mal");
    }

    // TODO: Pass Validations (ONLY THOSE VALIDATIONS THAT DON'T USE _userInfrastructure) -> TO API LAYER (Request and Response)
    private bool ExistsByEmailValidation(string email)
    {
        return _userInfrastructure.ExistsByEmail(email);
    }
    private static void IsValidCreate(User user)
    {
        if (string.IsNullOrEmpty(user.Name))throw new Exception("Name is required");
        if (string.IsNullOrEmpty(user.Email))throw new Exception("Email is required");
        if (string.IsNullOrEmpty(user.Password))throw new Exception("Password is required");
    }
    private bool ExistsByIdValidation(int id)
    {
        return _userInfrastructure.ExistsById(id);
    }
    private static void IsValidUpdate(UserDto user)
    {
        if (user.Name.Length > 50) throw new Exception("Name has to be less than 50 characters");
        if (user.Email.Length > 50) throw new Exception("Email has to be less than 50 characters");
    }
    private bool AllowedEmailUpdate(int id, UserDto user)
    {
        if (_userInfrastructure.ExistsByIdAndEmail(id, user.Email)) return true;
        if (ExistsByEmailValidation(user.Email)) throw new Exception("A user already exists with this email");
        return true;
    }
}