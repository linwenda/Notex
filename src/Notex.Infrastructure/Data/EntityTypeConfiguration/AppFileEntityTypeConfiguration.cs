using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Notex.Infrastructure.FileProviders;

namespace Notex.Infrastructure.Data.EntityTypeConfiguration;

public class AppFileEntityTypeConfiguration : IEntityTypeConfiguration<AppFile>
{
    public void Configure(EntityTypeBuilder<AppFile> builder)
    {
        builder.Property(p => p.Name).IsRequired().HasMaxLength(256);
        builder.Property(p => p.ContentType).HasMaxLength(64);
    }
}