using Funzone.Domain.Users;

namespace Funzone.Domain.SharedKernel
{
    public interface IHaveAuthorId
    {
        public UserId AuthorId { get; }
    }
}