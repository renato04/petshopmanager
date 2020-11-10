using PetShop.Domain.Application.Clients.Commands;
using PetShop.Domain.Application.Clients.Dto;
using PetShop.Domain.Models;

namespace PetShop.Domain.Application.Mapping
{
    public class PetShopMappingProfile : AutoMapper.Profile
    {
        public PetShopMappingProfile()
        {
            CreateMap<CreateClientCommand, Client>()
                .ReverseMap();

            CreateMap<CreateClientResponse, Client>()
                .ReverseMap();

            CreateMap<UpdateClientCommand, Client>()
                .ReverseMap();

            CreateMap<UpdateClientResponse, Client>()
                .ReverseMap();

            CreateMap<ClientDto, Client>()
                .ReverseMap();

            CreateMap<PetDto, Pet>()
                .ReverseMap();

            CreateMap<AddPetCommand, Pet>();
            CreateMap<Pet, AddPetResponse>();
            CreateMap<UpdatePetCommand, Pet>();
            CreateMap<Pet, UpdatePetResponse>();

        }
    }
}
