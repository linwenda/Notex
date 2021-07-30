using System;
using System.Threading;
using System.Threading.Tasks;
using MarchNote.Application.Configuration.Commands;
using MarchNote.Application.Configuration.Responses;
using MarchNote.Domain.NoteAggregate;
using MarchNote.Domain.Users;

namespace MarchNote.Application.Notes.Commands
{
    public class CreateNoteCommandHandler : ICommandHandler<CreateNoteCommand,MarchNoteResponse<Guid>>
    {
        private readonly IUserContext _userContext;
        private readonly INoteRepository _noteRepository;

        public CreateNoteCommandHandler(IUserContext userContext, INoteRepository noteRepository)
        {
            _userContext = userContext;
            _noteRepository = noteRepository;
        }

        public async Task<MarchNoteResponse<Guid>> Handle(CreateNoteCommand request, CancellationToken cancellationToken)
        {
            var note = new Note(new NoteId(Guid.NewGuid()));

            note.Create(_userContext.UserId, request.Title, request.Content);

            await _noteRepository.SaveAsync(note, cancellationToken);

            return new MarchNoteResponse<Guid>(note.Id.Value);
        }
    }
}