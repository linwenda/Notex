using Funzone.PhotoAlbums.Domain.Albums;
using Funzone.PhotoAlbums.Domain.SharedKenel;
using Funzone.PhotoAlbums.Domain.Users;
using Funzone.PhotoAlbums.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Funzone.PhotoAlbums.Infrastructure.Domain.Albums
{
    public class AlbumEntityTypeConfiguration : IEntityTypeConfiguration<Album>
    {
        public void Configure(EntityTypeBuilder<Album> builder)
        {
            builder.ToTable("Albums", PhotoAlbumsContext.DefaultSchema);

            builder.HasKey(a => a.Id);

            builder.Property(a => a.Id)
                .HasConversion(v => v.Value, v => new AlbumId(v));

            builder.Property(a => a.Name)
                .IsRequired()
                .HasColumnType("varchar(50)");

            builder.Property(a => a.UserId)
                .HasConversion(v => v.Value, v => new UserId(v));

            builder.OwnsOne(a => a.Visibility, v =>
            {
                v.Property(p => p.Value).HasColumnName("Visibility");
            });

            //builder.Property(a => a.Visibility)
            //    .HasConversion(v => v.Value, v => new Visibility(v));
        }
    }
}