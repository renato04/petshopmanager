using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PetShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShop.Api.Pet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpGet]
        public IActionResult Test()
        {
            //var user = new User { Password = "1234", Username = "renato" };
            //var p = new PasswordHasher();
            //var hash = p.HashPassword(new User(), user.Password);

            return Ok("");

        }
    }
}
