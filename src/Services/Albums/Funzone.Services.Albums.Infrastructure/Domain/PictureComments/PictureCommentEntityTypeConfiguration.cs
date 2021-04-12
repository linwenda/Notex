using Funzone.Services.Albums.Domain.PictureComments;
using Funzone.Services.Albums.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Funzone.Services.Albums.Infrastructure.Domain.PictureComments
{
    public class PictureCommentEntityTypeConfiguration : IEntityTypeConfiguration<PictureComment>
    {
        public void Configure(EntityTypeBuilder<PictureComment> builder)
        {
            builder.ToTable("PictureComments", AlbumsContext.DefaultSchema);

            builder.Property(p => p.Comment)
                .IsRequired()
                .HasColumnType("varchar(1024)");
        }
    }
}