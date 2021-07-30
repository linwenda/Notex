using System;
using System.Threading;
using System.Threading.Tasks;
using MarchNote.Application.Configuration.Commands;
using MarchNote.Application.Configuration.Responses;
using MarchNote.Domain.NoteAggregate;
using MarchNote.Domain.Users;

namespace MarchNote.Application.Notes.Commands
{
    public class DraftOutNoteCommandHandler : ICommandHandler<DraftOutNoteCommand, MarchNoteResponse<Guid>>
    {
        private readonly IUserContext _userContext;
        private readonly INoteRepository _noteRepository;

        public DraftOutNoteCommandHandler(IUserContext userContext, INoteRepository noteRepository)
        {
            _userContext = userContext;
            _noteRepository = noteRepository;
        }

        public async Task<MarchNoteResponse<Guid>> Handle(DraftOutNoteCommand request, CancellationToken cancellationToken)
        {
            var note = await _noteRepository.LoadAsync(new NoteId(request.NoteId), cancellationToken);

            var newNote = note.DraftOut(_userContext.UserId);

            await _noteRepository.SaveAsync(newNote, cancellationToken);

            return new MarchNoteResponse<Guid>(newNote.Id.Value);
        }
    }
}