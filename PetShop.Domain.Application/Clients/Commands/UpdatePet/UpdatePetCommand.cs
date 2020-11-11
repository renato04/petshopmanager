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

namespace PetShop.Domain.Application.Clients.Commands.UpdatePet
{
    public record UpdatePetCommand(string Name, Guid ClientId) : IRequestWrapper<UpdatePetResponse>;
    public record UpdatePetResponse(string Name, Guid ClientId);


    public class UpdatePetCommandHandler : IHandlerWrapper<UpdatePetCommand, UpdatePetResponse>
    {

        private readonly IPetRepository _petRepository;
        private readonly IMapper _maper;

        public UpdatePetCommandHandler(IMapper maper, IPetRepository petRepository)
        {
            _petRepository = petRepository;
            _maper = maper;
        }

        public async Task<Response<UpdatePetResponse>> Handle(UpdatePetCommand request, CancellationToken cancellationToken)
        {
            var pet = _maper.Map<Pet>(request);

            await _petRepository.Update(pet);

            return Response.Ok(_maper.Map<UpdatePetResponse>(pet), "Pet Updated");
        }
    }
}
