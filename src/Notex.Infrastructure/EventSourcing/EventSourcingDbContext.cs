using Microsoft.EntityFrameworkCore;
using Notex.Infrastructure.EventSourcing.EntityTypeConfiguration;

namespace Notex.Infrastructure.EventSourcing;

public class EventSourcingDbContext : DbContext, IEventSourcingDbContext
{
    public EventSourcingDbContext(DbContextOptions<EventSourcingDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new EventEntityConfiguration());
        modelBuilder.ApplyConfiguration(new MementoEntityConfiguration());
    }
}