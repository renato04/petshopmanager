using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PetShop.Domain.Models.Repository
{
    public interface IUserRepository
    {
        Task<User?> Get(string username, string password);
    }
}