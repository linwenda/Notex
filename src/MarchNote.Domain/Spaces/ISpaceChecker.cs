using System.Threading.Tasks;
using MarchNote.Domain.SeedWork;
using MarchNote.Domain.Users;

namespace MarchNote.Domain.Spaces
{
    public interface ISpaceChecker : IDomainService
    {
        Task<int> CalculateSpaceCountAsync(UserId userId);
        Task<bool> IsUniqueNameAsync(UserId userId, string spaceName);
    }
}