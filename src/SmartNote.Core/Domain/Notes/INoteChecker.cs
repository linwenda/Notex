namespace SmartNote.Core.Domain.Notes
{
    public interface INoteChecker : IDomainService
    {
        Task<bool> IsAuthorAsync(Guid noteId, Guid userId);
        Task<bool> IsWriterAsync(Guid noteId, Guid userId);
    }
}