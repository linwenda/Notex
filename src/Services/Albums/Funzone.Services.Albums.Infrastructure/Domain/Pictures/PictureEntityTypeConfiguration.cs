using Funzone.Services.Albums.Domain.Pictures;
using Funzone.Services.Albums.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Funzone.Services.Albums.Infrastructure.Domain.Pictures
{
    public class PictureEntityTypeConfiguration : IEntityTypeConfiguration<Picture>
    {
        public void Configure(EntityTypeBuilder<Picture> builder)
        {
            builder.ToTable("Pictures", AlbumsContext.DefaultSchema);
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Title)
                .IsRequired()
                .HasColumnType("varchar(50)");

            builder.Property(p => p.Link)
                .IsRequired()
                .HasColumnType("varchar(512)");

            builder.Property(p => p.ThumbnailLink)
                .HasColumnType("varchar(512)");

            builder.Property(p => p.Description)
                .IsRequired()
                .HasColumnType("varchar(512)");
        }
    }
}