using Microsoft.EntityFrameworkCore;

namespace BuecherVerwaltungEmpty.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

    public DbSet<Buch> Buecher { get; set; }
}