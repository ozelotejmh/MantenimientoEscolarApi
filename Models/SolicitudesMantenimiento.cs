using System.ComponentModel.DataAnnotations;

namespace MantenimientoEscolarApi.Models
{
    public class SolicitudesMantenimiento
    {
        [Key]
        public int IdSolicitud { get; set; }
        public int? UsuarioId { get; set; }
        public string? NombreUsuario { get; set; }
        public string? Correo { get; set; }
        public string? TipoUsuario { get; set; }
        public int? CategoriaId { get; set; }
        public string? NombreCategoria { get; set; }
        public string? Descripcion { get; set; }
        public string? Ubicacion { get; set; }
        public DateTime? Fecha { get; set; }
        public string? Estado { get; set; }
    }
}
