using EntrepeneurHero.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EntrepeneurHero.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // Define las tablas (DbSet) para cada entidad
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Venta> Ventas { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Presupuesto> Presupuestos { get; set; }
        public DbSet<ProductoVenta> ProductosVentas { get; set; }
        public DbSet<ProductoPresupuesto> ProductosPresupuestos { get; set; }

        // Configuraciones adicionales (opcional)
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configurar la clave primaria compuesta para las tablas intermedias
            modelBuilder.Entity<ProductoVenta>().HasKey(pv => new { pv.ProductoId, pv.VentaId });
            modelBuilder.Entity<ProductoPresupuesto>().HasKey(pp => new { pp.ProductoId, pp.PresupuestoId });
        }
    }
}
