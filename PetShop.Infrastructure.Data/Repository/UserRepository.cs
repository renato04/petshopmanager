using PetShop.Domain.Models;
using PetShop.Domain.Models.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShop.Infrastructure.Data.Repository
{
    public class UserRepository : IUserRepository
    {
        public Task<User?> Get(string username, string password)
        {
            return Task.Run(() => 
            {
                var users = new List<User>();
                users.Add(new User { Id = "aaaaa", Username = "batman", Password = "batman", /*Role = "manager"*/ });
                users.Add(new User { Id = "aaaaa", Username = "robin", Password = "robin",/* Role = "employee"*/ });
                return users.Where(x => x.Username.ToLower() == username.ToLower() && x.Password == x.Password).FirstOrDefault();
            });
        }
    }
}
