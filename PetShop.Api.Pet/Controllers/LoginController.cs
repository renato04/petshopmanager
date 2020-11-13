using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PetShop.Api.Pet.Models;
using PetShop.Authorization;

using PetShop.Domain.Models.Repository;
using System.Threading.Tasks;

namespace PetShop.Api.Pet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly IUserRepository _userRepository;

        public LoginController(ITokenService tokenService, IUserRepository userRepository)
        {
            _tokenService = tokenService;
            _userRepository = userRepository;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("login")]
        public async Task<ActionResult<LoggedUser>> Authenticate([FromBody] User model,
           [FromServices] UserManager<IdentityUser> userManager,
            [FromServices] SignInManager<IdentityUser> signInManager,
            [FromServices] SigningConfigurations signingConfigurations,
            [FromServices] TokenConfigurations tokenConfigurations)
        {
            var userIdentity = userManager
                .FindByNameAsync(model.Username).Result;
            if (userIdentity != null)
            {
                // Efetua o login com base no Id do usuário e sua senha
                var resultadoLogin = signInManager
                    .CheckPasswordSignInAsync(userIdentity, model.Password, false)
                    .Result;
                if (!resultadoLogin.Succeeded)
                {
                    return NotFound("Usuário ou senha inválidos");
                    // Verifica se o usuário em questão possui
                    // a role Acesso-APIAlturas
                    //credenciaisValidas = userManager.IsInRoleAsync(
                    //    userIdentity, Roles.ROLE_API_ALTURAS).Result;
                }

                var token = _tokenService.GenerateToken(userIdentity);

                return new LoggedUser(userIdentity.UserName, "", token);
            }
            else
            {
                return NotFound("Usuário ou senha inválidos");
            }

   
        }
    }
}
