using Microsoft.EntityFrameworkCore;
using WebApplication1;

public class AppDbContext : DbContext
{
    public DbSet<Kontrahenci> Kontrahent { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
}
