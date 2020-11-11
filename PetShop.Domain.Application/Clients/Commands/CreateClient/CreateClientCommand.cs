using System;
using MediatR;
using System.Collections.Generic;
using System.Text;
using PetShop.Domain.Application.Wrappers;
using System.Threading.Tasks;
using System.Threading;
using AutoMapper;
using PetShop.Domain.Models.Repository;
using PetShop.Domain.Models;
using System.ComponentModel.DataAnnotations;
using PetShop.Domain.Application.Clients.Dto;

namespace PetShop.Domain.Application.Clients.Commands.CreateClient
{
    public record CreateClientCommand : IRequestWrapper<CreateClientResponse>
    {
        public string Name { get; init; } = string.Empty;
    }

    public record CreateClientResponse
    {
        public Guid Id { get; init; }
        public string Name { get; init; } = string.Empty;
    }

    public class CreateClientCommandHandler : IHandlerWrapper<CreateClientCommand, CreateClientResponse>
    {

        private readonly IClientRepository _clientRepository;
        private readonly IMapper _maper;

        public CreateClientCommandHandler(IMapper maper, IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
            _maper = maper;
        }

        public async Task<Response<CreateClientResponse>> Handle(CreateClientCommand request, CancellationToken cancellationToken)
        {
            var client = _maper.Map<Client>(request);

            await _clientRepository.Add(client);

            return Response.Ok(_maper.Map<CreateClientResponse>(client), "Client Created");
        }
    }
}
