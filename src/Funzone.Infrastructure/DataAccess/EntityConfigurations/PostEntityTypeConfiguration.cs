using Funzone.Domain.Posts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Funzone.Infrastructure.DataAccess.EntityConfigurations
{
    public class PostEntityTypeConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.ToTable("Posts");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Title)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(p => p.Content)
                .HasMaxLength(2054);

            builder.OwnsOne(p => p.Type, s =>
            {
                s.Property(sp => sp.Value)
                    .HasColumnName("Type")
                    .HasMaxLength(20);
            });

            builder.OwnsOne(p => p.Status, s =>
            {
                s.Property(sp => sp.Value)
                    .HasColumnName("Status")
                    .HasMaxLength(20);
            });

            builder.OwnsMany(p => p.PostReviews, r =>
            {
                r.WithOwner().HasForeignKey(rp => rp.PostId);

                r.ToTable("PostReviews");

                r.Property(rp => rp.Detail).HasMaxLength(256);

                r.OwnsOne(p => p.PostStatus, rp =>
                {
                    rp.Property(sp => sp.Value)
                        .HasColumnName("PostStatus")
                        .HasMaxLength(20);
                });
            });
        }
    }
}