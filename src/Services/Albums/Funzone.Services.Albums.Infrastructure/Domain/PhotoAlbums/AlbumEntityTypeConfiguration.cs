using Funzone.Services.Albums.Domain.PhotoAlbums;
using Funzone.Services.Albums.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Funzone.Services.Albums.Infrastructure.Domain.PhotoAlbums
{
    public class AlbumEntityTypeConfiguration : IEntityTypeConfiguration<Album>
    {
        public void Configure(EntityTypeBuilder<Album> builder)
        {
            builder.ToTable("Albums", AlbumsContext.DefaultSchema);

            builder.HasKey(a => a.Id);

            builder.Property(a => a.Name)
                .IsRequired()
                .HasColumnType("varchar(50)");

            builder.OwnsOne(a => a.Visibility, v =>
            {
                v.Property(p => p.Value).HasColumnName("Visibility");
            });
        }
    }
}