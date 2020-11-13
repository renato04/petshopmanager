using PetShop.Domain.Models;

namespace PetShop.Authorization
{
    public interface ITokenService
    {
        string GenerateToken(User user);
    }
}