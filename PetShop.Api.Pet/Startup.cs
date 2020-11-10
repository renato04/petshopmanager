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
using PetShop.Domain.Application.Mapping;
using PetShop.Infrastructure.Data.Context;
using PetShop.Infrastructure.Data.Repository;
using PetShop.Infrastructure.IoC;
using Microsoft.OpenApi.Models;
using PetShop.Domain.Application.Clients.Dto;
using MediatR;
using PetShop.Domain.Application.Clients.Commands;

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
            services.AddMvc();
            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
                c.EnableAnnotations();
            });

            services.AddSingleton(PetShopMappingConfiguration.GetPetShopMappings());
            
            services.AddPetShopDependecies();
            services.AddDbContext<PetShopDbContext>(options =>
            {
                options.UseSqlServer(
                    Configuration.GetConnectionString("PetShopConnection"));
            });

            services.AddMediatR(typeof(AddPetCommandHandler).Assembly);


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

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
