using FluentValidation;
using MediatR;
using Tienda.MicroServicios.Autor.Api.Percistence;

namespace Tienda.MicroServicios.Autor.Api.Application
{
    public class Nuevo
    {
        public class Ejecuta : IRequest
        {
            public string Nombre { get; set; }
            public string Apellido { get; set; }
            public DateTime FechaNacimiento { get; set; }
        }
        public class EjecutaValidator : AbstractValidator<Ejecuta>
        {
            public EjecutaValidator()
            {
                RuleFor(x => x.Nombre).NotEmpty().WithMessage("El nombre es obligatorio");
                RuleFor(x => x.Apellido).NotEmpty().WithMessage("El apellido es obligatorio");
                RuleFor(x => x.FechaNacimiento).NotEmpty().WithMessage("La fecha de nacimiento es obligatoria");
            }
        }
        public class Manejador : IRequestHandler<Ejecuta>
        {
            private readonly ContextoAutor _contexto;
            public Manejador(ContextoAutor contexto)
            {
                _contexto = contexto;
            }
            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var autorLibro = new Model.AutorLibro
                {
                    Nombre = request.Nombre,
                    Apellido = request.Apellido,
                    FechaNacimiento = DateTime.SpecifyKind(request.FechaNacimiento, DateTimeKind.Utc),
                    AutorLibroGuid = Convert.ToString(Guid.NewGuid()) // Genera un GUID único para el autor
                };
                _contexto.AutorLibros.Add(autorLibro);
                var resultado = await _contexto.SaveChangesAsync();
                if (resultado > 0)
                {
                    return Unit.Value;
                }
                throw new Exception("No se pudo insertar el autor");
            }
        }
    }

}
