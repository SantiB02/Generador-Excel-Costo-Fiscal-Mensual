using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace SubsidiosClientes.Data
{
    internal class SubsidiosContextFactory : IDesignTimeDbContextFactory<SubsidiosContext>
    {
        public SubsidiosContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<SubsidiosContext>();
            optionsBuilder.UseSqlite("Data Source=SubsidiosClientes.db", b => b.MigrationsAssembly("SubsidiosClientes"));
            return new SubsidiosContext(optionsBuilder.Options);
        }
    }
}
