using Microsoft.EntityFrameworkCore;
using SubsidiosClientes.Data.Entities;

namespace SubsidiosClientes.Data
{
    public class SubsidiosContext : DbContext
    {
        public DbSet<Prestamo> Prestamos { get; set; }
        public DbSet<Cuota> Cuotas { get; set; }
        public SubsidiosContext(DbContextOptions<SubsidiosContext> dbContextOptions): base(dbContextOptions)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //relación uno (préstamo) a muchos (cuotas)
            modelBuilder.Entity<Prestamo>()
                .HasMany(p => p.Cuotas)
                .WithOne(c => c.Prestamo)
                .HasForeignKey(c => c.IdPrestamo);
        }
    }
}
