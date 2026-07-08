using Domain.Mappings;
using Microsoft.EntityFrameworkCore;

namespace JsonMapper.Infrastructure.PostgresDbConfiguration;

public class AppDbContext : DbContext
{
    public DbSet<JsonMapping> JsonMappings { get; set; }

    public DbSet<JsonFieldMapping> JsonFieldMappings { get; set; }

    public AppDbContext(
        DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }
    
    protected override void OnModelCreating(
        ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(AppDbContext).Assembly);
    }
}