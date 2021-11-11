using System;
using System.Threading.Tasks;
using MarchNote.Domain.Notes;
using MarchNote.Domain.Notes.ReadModels;
using MarchNote.Domain.Shared;

namespace MarchNote.Application.Notes
{
    public class NoteChecker : INoteChecker
    {
        private readonly IRepository<NoteReadModel> _noteRepository;

        public NoteChecker(IRepository<NoteReadModel> noteRepository)
        {
            _noteRepository = noteRepository;
        }

        public Task<bool> IsAuthorAsync(Guid noteId, Guid userId)
        {
            return _noteRepository.AnyAsync(n => n.Id == noteId && n.AuthorId == userId);
        }
    }
}