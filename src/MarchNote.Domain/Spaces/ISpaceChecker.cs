using System;
using System.Threading.Tasks;
using MarchNote.Domain.SeedWork;

namespace MarchNote.Domain.Spaces
{
    public interface ISpaceChecker : IDomainService
    {
        Task<int> CalculateSpaceCountAsync(Guid userId);
        Task<bool> IsUniqueNameAsync(Guid userId, string spaceName);
    }
}