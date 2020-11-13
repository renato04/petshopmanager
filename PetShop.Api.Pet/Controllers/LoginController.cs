using Microsoft.AspNetCore.Authorization;
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
        public async Task<ActionResult<LoggedUser>> Authenticate([FromBody] User model)
        {
            var user = await _userRepository.Get(model.Username, model.Password);

            if (user == null)
                return NotFound("Usuário ou senha inválidos");

            var token = _tokenService.GenerateToken(user);

            return new LoggedUser(user.Username,  user.Role, token);
        }
    }
}
