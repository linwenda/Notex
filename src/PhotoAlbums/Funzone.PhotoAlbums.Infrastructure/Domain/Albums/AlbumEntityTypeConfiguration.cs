using Funzone.PhotoAlbums.Domain.Albums;
using Funzone.PhotoAlbums.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Funzone.PhotoAlbums.Infrastructure.Domain.Albums
{
    public class AlbumEntityTypeConfiguration : IEntityTypeConfiguration<Album>
    {
        public void Configure(EntityTypeBuilder<Album> builder)
        {
            //TODO: Add schema
            builder.ToTable("albums");

            builder.HasKey(a => a.Id);

            builder.Property(a => a.Id)
                .HasColumnName("id")
                .HasColumnType("char(36)")
                .HasConversion(i => i.Value, v => new AlbumId(v));

            builder.Property(a => a.Name)
                .HasColumnName("name")
                .HasColumnType("varchar(50)");

            builder.Property(a => a.UserId)
                .HasColumnName("user_id")
                .HasColumnType("char(36)")
                .HasConversion(i => i.Value, v => new UserId(v));
        }
    }
}