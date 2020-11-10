using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using MediatR;
using PetShop.Domain.Application.Clients.Commands;
using PetShop.Domain.Application.Clients.Queries;
using PetShop.Domain.Application;
using Swashbuckle.AspNetCore.Annotations;
using PetShop.Domain.Application.Clients.Dto;
using PetShop.Domain.Application.Clients.Commands.UpdateClient;
using PetShop.Domain.Application.Clients.Commands.CreateClient;
using PetShop.Domain.Application.Clients.Commands.AddPet;

namespace PetShop.Api.Pet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ClientController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPut]
        [SwaggerOperation(Summary = "Update a client")]
        [SwaggerResponse(201, Description = "The client was successfully created.", Type = typeof(Response<ClientDto>))]
        public async Task<ActionResult<Response<UpdateClientResponse>>> UpdateClient([FromBody] UpdateClientCommand request)
        {
            var response = await _mediator.Send(request);

            if (response.Error)
            {
                return BadRequest(response.Message);
            }
            else
            {
                return Created(nameof(GetClient), response);

            }
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Create a client")]
        [SwaggerResponse(201, Description = "The client was successfully created.", Type = typeof(Response<ClientDto>))]
        public async Task<ActionResult<Response<CreateClientResponse>>> CreateClient([FromBody] CreateClientCommand request)
        {
            var response = await _mediator.Send(request);

            if(response.Error)
            {
                return BadRequest(response.Message);
            }
            else
            {
                return Created(nameof(GetClient), response);

            }
        }

        [HttpPost]
        [Route("{clientId}/pet")]
        [SwaggerOperation(Summary = "Add a pet to a client")]
        [SwaggerResponse(201, Description = "The pet was successfully added.", Type = typeof(Response<PetDto>))]
        public async Task<ActionResult<Response<AddPetResponse>>> AddPet(Guid clientId, [FromBody] AddPetCommand request)
        {
            request.ClientId = clientId;
            var response = await _mediator.Send(request);

            if (response.Error)
            {
                return BadRequest(response.Message);
            }
            else
            {
                return Created(nameof(GetClient), response);

            }
        }

        [HttpGet]
        [Route("{clientId}")]
        [SwaggerOperation(Summary = "Get a client")]
        [SwaggerResponse(200, Description = "The client.", Type = typeof(Response<ClientDto>))]
        public async Task<ActionResult<Response<ClientDto>>> GetClient(Guid clientId)
        {
            var response = await _mediator.Send(new GetClientByIdQuery { Id = clientId });

            if (response.Error)
            {
                return BadRequest(response.Message);
            }
            else
            {
                return Ok(response);

            }
        }
    }
}
