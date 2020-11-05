using PetShop.Domain.Application.Handlers.ClientHandlers;
using PetShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetShop.Domain.Application.Mapping
{
    public class PetShopMappingProfile : AutoMapper.Profile
    {
        public PetShopMappingProfile()
        {
            CreateMap<AddClientRequest, Client>()
                .ReverseMap();

            CreateMap<AddClientResponse, Client>()
                .ReverseMap();

        }
    }
}
