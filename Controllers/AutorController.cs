using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tienda.MicroServicios.Autor.Api.Application;

namespace Tienda.MicroServicios.Autor.Api.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AutoresController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly WriteDBContext _writeContext;
        private readonly ReadDBContext _readContext;

        public AutoresController(IMediator mediator, WriteDBContext writeContext, ReadDBContext readContext)
        {
            _mediator = mediator;
            _writeContext = writeContext;
            _readContext = readContext;
        }

        // GET: api/autores
        [HttpGet]
        public async Task<IActionResult> GetAutores()
        {
            var result = await _mediator.Send(new Consulta.Ejecuta());
            return Ok(result);

        }

        // GET: api/autores/{guid}
        [HttpGet("{guid}")]
        public async Task<IActionResult> GetAutorPorId(string guid)
        {
            var result = await _mediator.Send(new ConsultarFiltro.AutorUnico { AutorGuid = guid });
            return Ok(result);
        }

        // GET: api/autores/nombre/{nombre}
        [HttpGet("nombre/{nombre}")]
        public async Task<IActionResult> GetAutorPorNombre(string nombre)
        {
            var result = await _mediator.Send(new ConsultaNombre.AutorNombre { Nombre = nombre });
            return Ok(result);
        }

        // POST: api/autores
        [HttpPost]
        public async Task<IActionResult> CrearAutor([FromBody] Nuevo.Ejecuta data)
        {
            var result = await _mediator.Send(data);
            return Ok(result);
        }

        


        //// PUT: api/autores/{guid}
        //[HttpPut("{guid}")]
        //public async Task<IActionResult> EditarAutor(string guid, [FromBody] Editar.Ejecuta data)
        //{
        //    data.AutorGuid = guid;
        //    var result = await _mediator.Send(data);
        //    return Ok(result);
        //}

        //// DELETE: api/autores/{guid}
        //[HttpDelete("{guid}")]
        //public async Task<IActionResult> EliminarAutor(string guid)
        //{
        //    var result = await _mediator.Send(new Eliminar.Ejecuta { AutorGuid = guid });
        //    return Ok(result);
        //}
    }
}