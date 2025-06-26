using MediatR;
using Microsoft.EntityFrameworkCore;
using Tienda.MicroServicios.Autor.Api.Percistence;

namespace Tienda.MicroServicios.Autor.Api.Application
{
    public class Consulta
    {
        public class Ejecuta : IRequest<List<AutorDto>>
        {
            public string AutorLibroGuid { get; set; }
        }
        public class Manejador : IRequestHandler<Ejecuta, List<AutorDto>>
        {
            private readonly ContextoAutor _contexto;
            public Manejador(ContextoAutor contexto)
            {
                _contexto = contexto;
            }
            public async Task<List<AutorDto>> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                 var autores = await _contexto.AutorLibros.ToListAsync(cancellationToken);
                //var autores = await _contexto.AutorLibros
                  //  .Where(x => x.AutorLibroGuid == request.AutorLibroGuid)
                    //.ToListAsync(cancellationToken);
                if (autores == null || !autores.Any())
                {
                    return new List<AutorDto>();
                }
                return autores.Select(autor => new AutorDto
                {
                    AutorLibroId = autor.AutorLibroId,
                    Nombre = autor.Nombre,
                    Apellido = autor.Apellido,
                    FechaNacimiento = autor.FechaNacimiento,
                    AutorLibroGuid = autor.AutorLibroGuid
                }).ToList();
            }
        }
    }
}
