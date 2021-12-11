using SmartNote.Core.Domain;
using SmartNote.Core.Domain.NoteCooperations;

namespace SmartNote.Core.Application.NoteCooperations
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