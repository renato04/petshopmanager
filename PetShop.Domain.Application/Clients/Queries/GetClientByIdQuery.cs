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
    public class GetClientByIdQuery : IRequestWrapper<ClientDto>
    {
        public Guid Id { get; set; }
    }

    public class GetClientByIdQueryHandler : IHandlerWrapper<GetClientByIdQuery, ClientDto>
    {
        private readonly IClientRepository _clientRepository;
        private readonly IMapper _maper;

        public GetClientByIdQueryHandler(IMapper maper, IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
            _maper = maper;
        }

        public async Task<Response<ClientDto>> Handle(GetClientByIdQuery query, CancellationToken cancellationToken)
        {
            var client = await _clientRepository.GetById(query.Id);

            return Response.Ok(_maper.Map<ClientDto>(client));

        }

    }



}