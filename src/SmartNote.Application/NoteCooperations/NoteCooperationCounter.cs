using SmartNote.Domain;
using SmartNote.Domain.NoteCooperations;

namespace SmartNote.Application.NoteCooperations
{
    public class NoteCooperationCounter : INoteCooperationCounter
    {
        private readonly IRepository<NoteCooperation> _cooperationRepository;

        public NoteCooperationCounter(IRepository<NoteCooperation> cooperationRepository)
        {
            _cooperationRepository = cooperationRepository;
        }

        public async Task<int> CountPendingAsync(Guid userId, Guid noteId)
        {
            return await _cooperationRepository.CountAsync(c =>
                c.SubmitterId == userId &&
                c.NoteId == noteId &&
                c.Status == NoteCooperationStatus.Pending);
        }
    }
}