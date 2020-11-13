using PetShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetShop.Api.Pet.Models
{
    public record LoggedUser(string Username, string Role, string Token);
    public record User(string Username, string Password);
}
