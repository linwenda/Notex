using Funzone.Services.Albums.Domain.Albums;
using Funzone.Services.Albums.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Funzone.Services.Albums.Infrastructure.Domain.Albums
{
    public class AlbumEntityTypeConfiguration : IEntityTypeConfiguration<Album>
    {
        public void Configure(EntityTypeBuilder<Album> builder)
        {
            builder.ToTable("Albums", AlbumsContext.DefaultSchema);

            builder.HasKey(a => a.Id);

            builder.Property(a => a.Title)
                .IsRequired()
                .HasColumnType("varchar(50)");

            builder.OwnsOne(a => a.Visibility, v =>
            {
                v.Property(p => p.Value)
                    .HasColumnType("varchar(20)")
                    .HasColumnName("Visibility");
            });
        }
    }
}