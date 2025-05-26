using MantenimientoEscolarApi.Models;
using Microsoft.EntityFrameworkCore;

namespace MantenimientoEscolarApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
        {
        }

        public DbSet<SolicitudesMantenimiento> SolicitudesMantenimiento { get; set; }
    }
}
