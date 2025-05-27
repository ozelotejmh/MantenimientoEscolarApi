using System.ComponentModel.DataAnnotations;
namespace MantenimientoEscolarApi.Models
{
    public class Usuarios
    {
        [Key]
        public int IdUsuario { get; set; }
        public string Nombre { get; set; }
        public string Correo { get; set; }
        public string TipoUsuario { get; set; }
    }
}
