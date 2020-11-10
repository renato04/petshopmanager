using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PetShop.Domain.Application.Mapping;
using PetShop.Infrastructure.Data.Context;
using PetShop.Infrastructure.IoC;
using Microsoft.OpenApi.Models;
using MediatR;
using PetShop.Domain.Application.Clients.Commands.AddPet;
using PetShop.Domain.Application.Clients.Commands.CreateClient;
using FluentValidation;
using PetShop.Api.Pet.Behaviours;
using PetShop.Domain.Application;

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
            services.AddValidatorsFromAssembly(typeof(CreateClientCommandValidator).Assembly);

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));


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
