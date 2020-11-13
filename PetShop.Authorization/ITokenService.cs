using Microsoft.AspNetCore.Identity;
using PetShop.Domain.Models;

namespace PetShop.Authorization
{
    public interface ITokenService
    {
        string GenerateToken(IdentityUser user);
    }
}