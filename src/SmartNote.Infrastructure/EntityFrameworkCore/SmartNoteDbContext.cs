using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;
using SmartNote.Core.Commons;
using SmartNote.Core.Domain;
using SmartNote.Core.Security.Users;
using SmartNote.Infrastructure.EntityFrameworkCore.EntityConfigurations;

namespace SmartNote.Infrastructure.EntityFrameworkCore
{
    public class SmartNoteDbContext : DbContext
    {
        private readonly string _connectionString;
        private readonly IClock _clock;
        private readonly ICurrentUser _currentUser;

        public SmartNoteDbContext(string connectionString, IClock clock, ICurrentUser currentUser)
        {
            _connectionString = connectionString;
            _clock = clock;
            _currentUser = currentUser;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString, b => b.CommandTimeout(10000));
            optionsBuilder.LogTo(Console.WriteLine);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(EventEntityConfiguration).Assembly);
            //modelBuilder.ApplyGlobalFilters<ICanSoftDelete>(e => !e.IsDeleted);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            OnBeforeSave();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void OnBeforeSave()
        {
            foreach (var entityEntry in ChangeTracker.Entries())
            {
                switch (entityEntry.State)
                {
                    case EntityState.Added:
                        SetCreationProperties(entityEntry);
                        break;
                    case EntityState.Modified:
                        SetModificationProperties(entityEntry);
                        break;
                }
            }
        }

        private void SetCreationProperties(EntityEntry entityEntry)
        {
            switch (entityEntry.Entity)
            {
                case IHasCreator entity:
                    entity.CreatorId = _currentUser.Id;
                    break;
                case IHasCreationTime entity:
                    entity.CreationTime = _clock.Now;
                    break;
            }
        }

        private void SetModificationProperties(EntityEntry entityEntry)
        {
            switch (entityEntry.Entity)
            {
                case IHasModifier entity:
                    entity.LastModifierId = _currentUser.Id;
                    break;
                case IHasModificationTime entity:
                    entity.LastModificationTime = _clock.Now;
                    break;
            }
        }
    }
}