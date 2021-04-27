using Funzone.Domain.PostVotes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Funzone.Infrastructure.DataAccess.EntityConfigurations
{
    public class PostVoteEntityTypeConfiguration : IEntityTypeConfiguration<PostVote>
    {
        public void Configure(EntityTypeBuilder<PostVote> builder)
        {
            builder.ToTable("PostVotes");

            builder.HasKey(p => p.Id);

            builder.OwnsOne(p => p.VoteType, v =>
            {
                v.Property(vp => vp.Value)
                    .HasColumnName("Type")
                    .HasMaxLength(10);
            });
        }
    }
}