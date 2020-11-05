using Microsoft.AspNetCore.Mvc;
using PetShop.Domain.Application.Handlers.ClientHandlers;
using PetShop.Domain.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetShop.Api.Pet.Controllers
{
    [Route("[controller]")]
    public class ClientController : ControllerBase
    {
        private readonly IAsyncRequestHandler<AddClientRequest, AddClientResponse> _addClientHandler;

        public ClientController(IAsyncRequestHandler<AddClientRequest, AddClientResponse> addClientHandler)
        {
            _addClientHandler = addClientHandler;
        }

        [HttpPost]
        public async Task<IActionResult> AddClient(AddClientRequest addClientRequest)
        {
            return  Ok(await _addClientHandler.HandleAsync(addClientRequest));
        }
    }
}
