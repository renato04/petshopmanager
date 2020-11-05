using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PetShop.Domain.Application;
using PetShop.Domain.Application.Handlers.ClientHandlers;
using PetShop.Domain.Application.Mapping;
using PetShop.Infrastructure.Data.Context;
using PetShop.Infrastructure.Data.Repository;
using PetShop.Infrastructure.IoC;

namespace PetShop.Api.Pet
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddSingleton(PetShopMappingConfiguration.GetPetShopMappings());
            
            services.AddPetShopDependecies();
            services.AddDbContext<PetShopDbContext>(options =>
            {
                options.UseSqlServer(
                    Configuration.GetConnectionString("PetShopConnection"));
            });

            services.AddTransient<IAsyncRequestHandler<AddClientRequest, AddClientResponse>, AddClientHandler>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
