namespace FastScooter.Domain.Interfaces;
public interface ITokenDomain
{
    string GenerateJwt(string username);
    string ValidateJwt(string token);
}