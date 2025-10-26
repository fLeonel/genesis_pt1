using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CazuelaChapina.Infrastructure.Persistence;

public class CazuelaChapinaDbContextFactory : IDesignTimeDbContextFactory<CazuelaChapinaDbContext>
{
    public CazuelaChapinaDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<CazuelaChapinaDbContext>();
        optionsBuilder.UseSqlite("Data Source=cazuela.db");

        return new CazuelaChapinaDbContext(optionsBuilder.Options);
    }
}
