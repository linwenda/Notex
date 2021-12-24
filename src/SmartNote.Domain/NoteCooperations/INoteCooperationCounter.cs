namespace SmartNote.Domain.NoteCooperations
{
    public interface INoteCooperationCounter : IDomainService
    {
        Task<int> CountPendingAsync(Guid userId, Guid noteId);
    }
}