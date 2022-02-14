using System.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;
using SmartNote.Application.Configuration.Commons;
using SmartNote.Application.Configuration.Security.Users;
using SmartNote.Domain;
using SmartNote.Infrastructure.EntityFrameworkCore.EntityConfigurations;

namespace SmartNote.Infrastructure.EntityFrameworkCore
{
    public class SmartNoteDbContext : DbContext
    {
        private readonly string _connectionString;
        private readonly IClock _clock;
        private readonly IGuidGenerator _guidGenerator;
        private readonly ICurrentUser _currentUser;
        private IDbContextTransaction _currentTransaction;
        public IDbContextTransaction GetCurrentTransaction() => _currentTransaction;
        public bool HasActiveTransaction => _currentTransaction != null;

        public SmartNoteDbContext(
            string connectionString,
            IClock clock,
            IGuidGenerator guidGenerator,
            ICurrentUser currentUser)
        {
            _connectionString = connectionString;
            _clock = clock;
            _guidGenerator = guidGenerator;
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

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            if (_currentTransaction != null) return null;

            _currentTransaction = await Database.BeginTransactionAsync(IsolationLevel.ReadCommitted);

            return _currentTransaction;
        }

        public async Task CommitTransactionAsync(IDbContextTransaction transaction)
        {
            if (transaction == null) throw new ArgumentNullException(nameof(transaction));
            if (transaction != _currentTransaction)
                throw new InvalidOperationException($"Transaction {transaction.TransactionId} is not current");

            try
            {
                await SaveChangesAsync();
                transaction.Commit();
            }
            catch
            {
                RollbackTransaction();
                throw;
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }

        private void RollbackTransaction()
        {
            try
            {
                _currentTransaction?.Rollback();
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
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

        private void SetCreationProperties(EntityEntry entry)
        {
            switch (entry.Entity)
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