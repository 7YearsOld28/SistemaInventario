using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SistemaInventario.Modelos;

namespace SistemaInventario.AccesosDatos.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Bodega> Bodegas { get; set; }
        public DbSet<Categoría> Categorías { get; set; }
        public DbSet<Marca> Marca { get; set; }
        public DbSet<Producto> Productos { get; set; }
    }
}