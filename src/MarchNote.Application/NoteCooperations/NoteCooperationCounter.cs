using System;
using System.Threading.Tasks;
using MarchNote.Domain.NoteCooperations;
using MarchNote.Domain.Notes;
using MarchNote.Domain.SeedWork;

namespace MarchNote.Application.NoteCooperations
{
    public class NoteCooperationCounter : INoteCooperationCounter
    {
        private readonly IRepository<NoteCooperation> _cooperationRepository;

        public NoteCooperationCounter(IRepository<NoteCooperation> cooperationRepository)
        {
            _cooperationRepository = cooperationRepository;
        }

        public async Task<int> CountPendingAsync(Guid userId, NoteId noteId)
        {
            return await _cooperationRepository.CountAsync(c =>
                c.SubmitterId == userId &&
                c.NoteId == noteId && 
                c.Status == NoteCooperationStatus.Pending);
        }
    }
}