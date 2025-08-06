using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MediatR;
using Tienda.MicroServicios.Autor.Api.Percistence;
using Tienda.MicroServicios.Autor.Api.Application;

namespace Tienda.MicroServicios.Autor.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCustomServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers().AddFluentValidation(cfg => cfg.RegisterValidatorsFromAssemblyContaining<Nuevo>());

            services.AddDbContext<WriteDBContext>(options =>
                options.UseMySql(configuration.GetConnectionString("WriteDB"), ServerVersion.AutoDetect(configuration.GetConnectionString("WriteDB"))));

            //services.AddDbContext<ReadDBContext>(options =>
            //    options.UseNpgsql(configuration.GetConnectionString("ReadDB")));


            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });
            });

            services.AddMediatR(typeof(Nuevo.Manejador).Assembly);
            services.AddAutoMapper(typeof(Consulta.Manejador).Assembly);

            return services;
        }
    }
}
