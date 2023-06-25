using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SimpleAPI.DataAccess.Configuration;
using SimpleAPI.DataAccess.Model;

namespace SimpleAPI.DataAccess;

public class ApplicationContext : DbContext
{
    public DbSet<User> Users { get; set; }

    public ApplicationContext(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("Default");

        Database.EnsureCreatedAsync();
    }

    private readonly string? _connectionString;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite(_connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfiguration());
    }
}
