using Funzone.Domain.ZoneRules;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Funzone.Infrastructure.DataAccess.EntityConfigurations
{
    public class ZoneRuleEntityTypeConfiguration : IEntityTypeConfiguration<ZoneRule>
    {
        public void Configure(EntityTypeBuilder<ZoneRule> builder)
        {
            builder.ToTable("ZoneRules");

            builder.HasKey(p => p.Id);
            
            builder.Property(p => p.Title)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(p => p.Description)
                .HasMaxLength(128);
        }
    }
}