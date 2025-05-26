using MantenimientoEscolarApi.Data;
using MantenimientoEscolarApi.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace MantenimientoEscolarApi.Service
{
    public class SolicitudService : ISolicitudService
    {
        private readonly AppDbContext _context;

        public SolicitudService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SolicitudesMantenimiento>> ObtenerTodasAsync()
        {
            return await _context.SolicitudesMantenimiento
                .FromSqlRaw("EXEC ConsultarTodasSolicitudes")
                .ToListAsync();
        }

        public async Task<SolicitudesMantenimiento> ObtenerPorIdAsync(int id)
        {
            var solicitudes = await _context.SolicitudesMantenimiento
                .FromSqlRaw("EXEC ConsultarSolicitudesPorUsuario @UsuarioId",
                    new SqlParameter("@UsuarioId", id))
                .ToListAsync();

            return solicitudes.FirstOrDefault();
        }

        public async Task CrearAsync(SolicitudesMantenimiento solicitud)
        {
            try
            {
                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC InsertarSolicitud @UsuarioId, @CategoriaId, @Descripcion, @Ubicacion, @Fecha, @Estado",
                    new SqlParameter("@UsuarioId", solicitud.UsuarioId),
                    new SqlParameter("@CategoriaId", solicitud.CategoriaId),
                    new SqlParameter("@Descripcion", solicitud.Descripcion),
                    new SqlParameter("@Ubicacion", solicitud.Ubicacion),
                    new SqlParameter("@Fecha", solicitud.Fecha),
                    new SqlParameter("@Estado", solicitud.Estado)
                );
            }
            catch (SqlException ex)
            {
                // Captura el mensaje personalizado del trigger
                throw new ApplicationException($"Error al crear solicitud: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Error inesperado al crear solicitud: {ex.Message}", ex);
            }
        }

        public async Task ActualizarAsync(SolicitudesMantenimiento solicitud)
        {
            try
            {
                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC ActualizarSolicitud @IdSolicitud, @Descripcion, @Ubicacion, @Estado",
                    new SqlParameter("@IdSolicitud", solicitud.IdSolicitud),
                    new SqlParameter("@Descripcion", solicitud.Descripcion),
                    new SqlParameter("@Ubicacion", solicitud.Ubicacion),
                    new SqlParameter("@Estado", solicitud.Estado)
                );
            }
            catch (SqlException ex)
            {
                // Captura el mensaje personalizado del trigger
                throw new ApplicationException($"Error al actualizar solicitud: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Error inesperado al actualizar solicitud: {ex.Message}", ex);
            }
        }

        public async Task EliminarAsync(int id)
        {
            try
            {
                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC EliminarSolicitud @IdSolicitud",
                    new SqlParameter("@IdSolicitud", id)
                );
            }
            catch (SqlException ex)
            {
                // Captura el mensaje personalizado del trigger
                throw new ApplicationException($"Error al eliminar solicitud: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Error inesperado al eliminar solicitud: {ex.Message}", ex);
            }
        }
    }
}
