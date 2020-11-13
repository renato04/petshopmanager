using Microsoft.Extensions.DependencyInjection;
using PetShop.Domain.Models.Repository;
using PetShop.Infrastructure.Data.Repository;
using System;

namespace PetShop.Infrastructure.IoC
{
    public static class PetShopServicesExtesion
    {
        public static void AddPetShopDependecies(this IServiceCollection services)
        {
            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<IPetRepository, PetRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
        }
    }
}
