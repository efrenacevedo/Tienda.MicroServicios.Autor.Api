using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Tienda.MicroServicios.Autor.Api.Model;
using Tienda.MicroServicios.Autor.Api.Percistence;

namespace Tienda.MicroServicios.Autor.Api.Application
{
    public class ConsultarFiltro
    {
        public class AutorUnico : IRequest<AutorDto> { 
            public string AutorGuid { get; set; }
        }
        public class Manejador : IRequestHandler<AutorUnico, AutorDto>
        {
            private readonly ReadDBContext _context;
            private readonly IMapper _mapper;
            public Manejador(ReadDBContext context, IMapper mapper)
            {
                this._context = context;
                this._mapper = mapper;
            }
            public async Task<AutorDto> Handle(AutorUnico request, CancellationToken cancellationToken)
            {
                var autor = await _context.AutorLibros.Where(p => p.AutorLibroGuid == request.AutorGuid).FirstOrDefaultAsync();
                if (autor == null)
                {
                    throw new Exception("No se encontro al autor");
                }
                var autorDto = _mapper.Map<AutorLibro, AutorDto>(autor);
                return autorDto;
            }
        }
    }
}
