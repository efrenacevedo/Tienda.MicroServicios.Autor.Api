using Tienda.MicroServicios.Autor.Api.Model;

namespace Tienda.MicroServicios.Autor.Api.Application
{
    public class AutorDto
    {
        public int AutorLibroId { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string AutorLibroGuid { get; set; }
    }
}
