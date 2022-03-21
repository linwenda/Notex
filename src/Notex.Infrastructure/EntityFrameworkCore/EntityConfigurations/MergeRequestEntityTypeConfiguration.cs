using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Notex.Core.Aggregates.MergeRequests.ReadModel;

namespace Notex.Infrastructure.EntityFrameworkCore.EntityConfigurations;

public class MergeRequestEntityTypeConfiguration : IEntityTypeConfiguration<MergeRequestDetail>
{
    public void Configure(EntityTypeBuilder<MergeRequestDetail> builder)
    {
        builder.ToTable("merge_requests");
        
        builder.HasKey(p => p.Id);
        
        builder.Property(p => p.Title).IsRequired().HasMaxLength(128);
        builder.Property(p => p.Description).HasMaxLength(512);
    }
}