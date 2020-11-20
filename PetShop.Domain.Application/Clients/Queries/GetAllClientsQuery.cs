using AutoMapper;
using MediatR;
using PetShop.Domain.Application.Clients.Dto;
using PetShop.Domain.Application.Wrappers;
using PetShop.Domain.Models.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PetShop.Domain.Application.Clients.Queries
{
    public class GetAllClientsQuery : IRequestWrapper<IEnumerable<ClientDto>>
    {
    }

    public class GetAllClientsQueryHandler : IHandlerWrapper<GetAllClientsQuery, IEnumerable<ClientDto>>
    {
        private readonly IClientRepository _clientRepository;
        private readonly IMapper _maper;

        public GetAllClientsQueryHandler(IMapper maper, IClientRepository clientRepository)
        {
            _clientRepository = clientRepository!;
            _maper = maper!;
        }

        public async Task<Response<IEnumerable<ClientDto>>> Handle(GetAllClientsQuery _, CancellationToken cancellationToken)
        {
            var client = await _clientRepository.GetAll();

            return Response.Ok(_maper.Map<IEnumerable<ClientDto>>(client));
        }
    }



}