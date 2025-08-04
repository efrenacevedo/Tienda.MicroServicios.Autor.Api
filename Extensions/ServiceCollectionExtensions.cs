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
            services.AddDbContext<ContextoAutor>(options => options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
            
            services.AddMediatR(typeof(Nuevo.Manejador).Assembly);
            services.AddAutoMapper(typeof(Consulta.Manejador).Assembly);
            return services;
        }
    }
}
