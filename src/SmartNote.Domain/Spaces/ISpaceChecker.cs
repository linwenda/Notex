namespace SmartNote.Domain.Spaces
{
    public interface ISpaceChecker : IDomainService
    {
        Task<int> CalculateSpaceCountAsync(Guid userId);
        Task<bool> IsUniqueNameAsync(Guid userId, string spaceName);
        Task<bool> IsUniqueNameAsync(Guid userId, Guid exceptSpaceId, string spaceName);
    }
}