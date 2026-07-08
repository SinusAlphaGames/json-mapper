using Domain.Mappings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JsonMapper.Infrastructure.PostgresDbConfiguration.Configurations;

public class JsonFieldMappingConfiguration
    : IEntityTypeConfiguration<JsonFieldMapping>
{
    public void Configure(
        EntityTypeBuilder<JsonFieldMapping> builder)
    {
        builder.ToTable("json_field_mappings");

        builder.HasKey(x => x.Id);


        builder.Property(x => x.Id)
            .ValueGeneratedNever();


        builder.Property(x => x.SourceField)
            .IsRequired()
            .HasMaxLength(500);


        builder.Property(x => x.TargetField)
            .IsRequired()
            .HasMaxLength(500);
    }
}