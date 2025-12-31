using backend.API.Entities;

namespace API.Interfaces;

public interface ITokenService
{
    string CreateToken(ApplicationUser user);
}