using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PetShop.Api.Pet.Models;
using PetShop.Domain.Models;
using Swashbuckle.AspNetCore.Annotations;
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
        [HttpPost]
        [AllowAnonymous]
        [SwaggerOperation(Summary = "Register a system user")]
        [SwaggerResponse(200, Description = "User registered", Type = typeof(IEnumerable<RegisterResultModel>))]
        [SwaggerResponse(409, Description = "Registration failed", Type = typeof(IEnumerable<RegisterResultModel>))]
        public async Task<ActionResult<RegisterResultModel>> Post([FromBody] RegisterUserModel model, [FromServices] UserManager<IdentityUser> userManager)
        {
            var identityUser = new IdentityUser { UserName = model.Username, Email = model.Username };

            var result = await userManager.CreateAsync(identityUser, model.Password);

            if(!result.Succeeded)
            {
                return Conflict(new RegisterResultModel{Succesful = false, Errors = result.Errors.Select(x => x.Description) });
            }

            return Ok(new RegisterResultModel { Succesful = true });

        }
    }
}
