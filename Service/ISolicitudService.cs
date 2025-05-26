using MantenimientoEscolarApi.Models;

namespace MantenimientoEscolarApi.Service
{
    public interface ISolicitudService
    {
        Task<IEnumerable<SolicitudesMantenimiento>> ObtenerTodasAsync();
        Task<IEnumerable<SolicitudesMantenimiento>> ObtenerPorIdAsync(int id);
        Task CrearAsync(SolicitudesMantenimiento solicitud);
        Task ActualizarAsync(SolicitudesMantenimiento solicitud);
        Task EliminarAsync(int id);
    }
}
