using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Notex.Core.Domain.MergeRequests.ReadModels;

namespace Notex.Infrastructure.Data.EntityTypeConfiguration;

public class MergeRequestEntityTypeConfiguration : IEntityTypeConfiguration<MergeRequestDetail>
{
    public void Configure(EntityTypeBuilder<MergeRequestDetail> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Title).IsRequired().HasMaxLength(128);
        builder.Property(p => p.Description).HasColumnType("text");
    }
}