using MantenimientoEscolarApi.Models;
using MantenimientoEscolarApi.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MantenimientoEscolarApi.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/solicitudes")]
    public class SolicitudesController : ControllerBase
    {
        private readonly ISolicitudService _service;

        public SolicitudesController(ISolicitudService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerTodas()
        {
            var solicitudes = await _service.ObtenerTodasAsync();
            return Ok(solicitudes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            var solicitud = await _service.ObtenerPorIdAsync(id);
            if (solicitud == null)
                return NotFound();
            return Ok(solicitud);
        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] CrearSolicitudDTO solicitud)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var entidad = new SolicitudesMantenimiento
            {
                UsuarioId = solicitud.usuarioId,
                CategoriaId = solicitud.categoriaId,
                Descripcion = solicitud.descripcion,
                Ubicacion = solicitud.ubicacion,
                Fecha = solicitud.fecha,
                Estado = solicitud.estado
            };

            try
            {
                await _service.CrearAsync(entidad);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(int id, [FromBody] ActualizarSolicitudDTO dto)
        {
            if (id != dto.idSolicitud)
                return BadRequest("El ID del cuerpo no coincide con el ID de la URL.");

            var entidad = new SolicitudesMantenimiento
            {
                IdSolicitud = dto.idSolicitud,
                Descripcion = dto.descripcion,
                Ubicacion = dto.ubicacion,
                Estado = dto.estado
            };

            try
            {
                await _service.ActualizarAsync(entidad);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            await _service.EliminarAsync(id);
            return NoContent();
        }
    }

}
