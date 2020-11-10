using AutoMapper;
using PetShop.Domain.Application.Wrappers;
using PetShop.Domain.Models;
using PetShop.Domain.Models.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PetShop.Domain.Application.Clients.Commands.AddPet
{
    public class AddPetCommand : IRequestWrapper<AddPetResponse>
    {
        public string Name { get; set; }
        public Guid ClientId { get; set; }
    }

    public class AddPetResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }

    public class AddPetCommandHandler : IHandlerWrapper<AddPetCommand, AddPetResponse>
    {

        private readonly IPetRepository _petRepository;
        private readonly IMapper _maper;

        public AddPetCommandHandler(IMapper maper, IPetRepository petRepository)
        {
            _petRepository = petRepository;
            _maper = maper;
        }

        public async Task<Response<AddPetResponse>> Handle(AddPetCommand request, CancellationToken cancellationToken)
        {
            var pet = _maper.Map<Pet>(request);

            await _petRepository.Add(pet);

            return Response.Ok(_maper.Map<AddPetResponse>(pet), "Pet Added");
        }
    }
}
