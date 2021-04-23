using System.Threading.Tasks;
using Funzone.Domain.SeedWork;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace Funzone.Application.Configuration.Data
{
    public interface ITransactionContext : IUnitOfWork
    {
        bool HasActiveTransaction { get; }

        DatabaseFacade Database { get; }

        Task<IDbContextTransaction> BeginTransactionAsync();

        Task CommitTransactionAsync(IDbContextTransaction transaction);
    }
}