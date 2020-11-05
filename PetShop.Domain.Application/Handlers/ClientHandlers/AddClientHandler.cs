using AutoMapper;
using PetShop.Domain.Models;
using PetShop.Domain.Models.Repository;
using System;
using System.Threading.Tasks;

namespace PetShop.Domain.Application.Handlers.ClientHandlers
{
    public class AddClientHandler : IAsyncRequestHandler<AddClientRequest, AddClientResponse>
    {
        private readonly IClientRepository _clientRepository;
        private readonly IMapper _maper;

        public AddClientHandler(IMapper maper, IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
            _maper = maper;
        }

        public async Task<AddClientResponse> HandleAsync(AddClientRequest data)
        {
            var client = _maper.Map<Client>(data);

            await _clientRepository.Add(client);

            return _maper.Map<AddClientResponse>(client);
        }

    }

    public class AddClientRequest
    {
        public string Name { get; set; }
    }

    public class AddClientResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
