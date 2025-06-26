using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Tienda.MicroServicios.Autor.Api.Model;
using Tienda.MicroServicios.Autor.Api.Percistence;

namespace Tienda.MicroServicios.Autor.Api.Application
{
    public class ConsultaNombre
    {
        public class AutorNombre : IRequest<AutorDto>
        {
            public string Nombre { get; set; }
        }
        public class Manejador : IRequestHandler<AutorNombre, AutorDto>
        {
            private readonly ContextoAutor _context;
            private readonly IMapper _mapper;
            public Manejador(ContextoAutor context, IMapper mapper)
            {
                this._context = context;
                this._mapper = mapper;
            }
            public async Task<AutorDto> Handle(AutorNombre request, CancellationToken cancellationToken)
            {
                var autor = await _context.AutorLibros.Where(p => p.Nombre.ToLower()==request.Nombre.ToLower()).FirstOrDefaultAsync();
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
