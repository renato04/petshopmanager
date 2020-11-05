using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetShop.Domain.Application.Mapping
{
    public class PetShopMappingConfiguration
    {
        public static IMapper GetPetShopMappings()
        {
            return new MapperConfiguration(c =>
            {
                c.AddProfile<PetShopMappingProfile>();
            })
            .CreateMapper();
        }
    }
}
