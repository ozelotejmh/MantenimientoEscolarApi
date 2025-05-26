using MantenimientoEscolarApi.Models;
using MantenimientoEscolarApi.Service;
using Microsoft.AspNetCore.Mvc;

namespace MantenimientoEscolarApi.Controllers
{
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
        public async Task<IActionResult> Crear([FromBody] SolicitudesMantenimiento solicitud)
        {
            await _service.CrearAsync(solicitud);
            return CreatedAtAction(nameof(ObtenerPorId), new { id = solicitud.IdSolicitud }, solicitud);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(int id, [FromBody] SolicitudesMantenimiento solicitud)
        {
            if (id != solicitud.IdSolicitud)
                return BadRequest();

            await _service.ActualizarAsync(solicitud);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            await _service.EliminarAsync(id);
            return NoContent();
        }
    }

}
