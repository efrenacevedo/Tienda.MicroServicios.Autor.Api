using AutoMapper;
using Tienda.MicroServicios.Autor.Api.Model;

namespace Tienda.MicroServicios.Autor.Api.Application
{
    public class MappingProfile:Profile
    {
        public MappingProfile() {
            CreateMap<AutorLibro, AutorDto>();
        }
    }
}
