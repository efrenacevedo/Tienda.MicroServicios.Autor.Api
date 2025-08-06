
using MediatR;
using Microsoft.EntityFrameworkCore;


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
            private readonly ILogger<Manejador> _logger;
            public readonly ReadDBContext _context;

            public Manejador(ILogger<Manejador> logger, ReadDBContext context)
            {
                _logger = logger;
                _context = context;
            }

            public async Task<List<AutorDto>> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
               

                var autores = await _context.AutorLibros
                    .AsNoTracking()
                    .ToListAsync(cancellationToken);

                var conn = _context.Database.GetDbConnection();
                _logger.LogInformation("Consultando autores desde: {Host}:{Port}", conn.DataSource, conn.ConnectionString);

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
                    AutorLibroGuid = autor.AutorLibroGuid,
                    Fuente = conn.DataSource
                }).ToList();
            }
        }
    }
}

