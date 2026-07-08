using Domain.Mappings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JsonMapper.Infrastructure.PostgresDbConfiguration.Configurations;

public class JsonMappingConfiguration : IEntityTypeConfiguration<JsonMapping>
{
    public void Configure(
        EntityTypeBuilder<JsonMapping> builder)
    {
        builder.ToTable("json_mappings");

        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Id)
            .ValueGeneratedNever();

        builder.Property(x => x.CreatedAt)
            .IsRequired();


        builder.HasMany(x => x.Fields)
            .WithOne(x => x.Mapping)
            .HasForeignKey(x => x.JsonMappingId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}