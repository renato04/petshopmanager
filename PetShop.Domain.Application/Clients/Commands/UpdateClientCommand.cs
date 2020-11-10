using AutoMapper;
using PetShop.Domain.Application.Clients.Dto;
using PetShop.Domain.Application.Wrappers;
using PetShop.Domain.Models;
using PetShop.Domain.Models.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PetShop.Domain.Application.Clients.Commands
{

    public class UpdateClientCommand : IRequestWrapper<UpdateClientResponse>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

    }


    public class UpdateClientResponse 
    {
        [Required]
        public Guid Id { get; set; }
        public string Name { get; set; }
    }


    public class UpdateClientCommandHandler : IHandlerWrapper<UpdateClientCommand, UpdateClientResponse>
    {

        private readonly IClientRepository _clientRepository;
        private readonly IMapper _maper;

        public UpdateClientCommandHandler(IMapper maper, IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
            _maper = maper;
        }

        public async Task<Response<UpdateClientResponse>> Handle(UpdateClientCommand request, CancellationToken cancellationToken)
        {
            var client = _maper.Map<Client>(request);

            await _clientRepository.Update(client);

            return Response.Ok(_maper.Map<UpdateClientResponse>(client), "Client Updated");
        }

    }
}
