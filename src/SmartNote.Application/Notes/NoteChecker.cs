using SmartNote.Domain;
using SmartNote.Domain.Notes;
using SmartNote.Domain.Notes.ReadModels;

namespace SmartNote.Application.Notes
{
    public class NoteChecker : INoteChecker
    {
        private readonly IRepository<NoteReadModel> _noteRepository;
        private readonly IRepository<NoteMemberReadModel> _noteMemberRepository;

        public NoteChecker(
            IRepository<NoteReadModel> noteRepository,
            IRepository<NoteMemberReadModel> noteMemberRepository)
        {
            _noteRepository = noteRepository;
            _noteMemberRepository = noteMemberRepository;
        }

        public async Task<bool> IsAuthorAsync(Guid noteId, Guid userId)
        {
            return await IsMemberAsync(noteId, userId, NoteMemberRole.Author);
        }

        public async Task<bool> IsWriterAsync(Guid noteId, Guid userId)
        {
            return await IsMemberAsync(noteId, userId, NoteMemberRole.Writer);
        }

        private async Task<bool> IsMemberAsync(Guid noteId, Guid userId, NoteMemberRole role)
        {
            return await _noteMemberRepository.AnyAsync(m => m.NoteId == noteId &&
                                                             m.MemberId == userId &&
                                                             m.Role == role.Value &&
                                                             m.IsActive);
        }
    }
}