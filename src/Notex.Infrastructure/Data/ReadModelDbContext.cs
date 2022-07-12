using Microsoft.EntityFrameworkCore;
using Notex.Core.Domain.Comments.ReadModels;
using Notex.Core.Domain.MergeRequests.ReadModels;
using Notex.Core.Domain.Notes.ReadModels;
using Notex.Core.Domain.Spaces.ReadModels;
using Notex.Infrastructure.Data.EntityTypeConfiguration;

namespace Notex.Infrastructure.Data;

public class ReadModelDbContext : DbContext
{
    public ReadModelDbContext(DbContextOptions<ReadModelDbContext> options) : base(options)
    {
    }

    public DbSet<SpaceDetail> Spaces { get; set; }
    public DbSet<NoteDetail> Notes { get; set; }
    public DbSet<NoteHistory> NoteHistories { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<MergeRequestDetail> MergeRequests { get; set; }
    public DbSet<CommentDetail> Comments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new CommentEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new MergeRequestEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new NoteEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new NoteHistoryEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new SpaceEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new TagEntityTypeConfiguration());
    }
}