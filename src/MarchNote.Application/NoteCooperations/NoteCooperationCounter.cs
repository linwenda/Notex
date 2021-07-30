using System.Linq;
using MarchNote.Domain.NoteAggregate;
using MarchNote.Domain.NoteCooperations;
using MarchNote.Domain.SeedWork;
using MarchNote.Domain.Users;

namespace MarchNote.Application.NoteCooperations
{
    public class NoteCooperationCounter : INoteCooperationCounter
    {
        private readonly IRepository<NoteCooperation> _cooperationRepository;

        public NoteCooperationCounter(IRepository<NoteCooperation> cooperationRepository)
        {
            _cooperationRepository = cooperationRepository;
        }
        
        public int CountPending(UserId userId, NoteId noteId)
        {
            return _cooperationRepository.Entities.Count(c =>
                c.SubmitterId == userId && c.NoteId == noteId && c.Status == NoteCooperationStatus.Pending);
        }
    }
}