using System.Threading;
using System.Threading.Tasks;
using MarchNote.Application.Configuration.Commands;
using MarchNote.Application.Configuration.Responses;
using MarchNote.Domain.NoteAggregate;
using MarchNote.Domain.Users;

namespace MarchNote.Application.Notes.Commands
{
    public class PublishNoteCommandHandler : ICommandHandler<PublishNoteCommand, MarchNoteResponse>
    {
        private readonly IUserContext _userContext;
        private readonly INoteRepository _noteRepository;

        public PublishNoteCommandHandler(IUserContext userContext, INoteRepository noteRepository)
        {
            _userContext = userContext;
            _noteRepository = noteRepository;
        }

        public async Task<MarchNoteResponse> Handle(PublishNoteCommand request, CancellationToken cancellationToken)
        {
            var note = await _noteRepository.LoadAsync(new NoteId(request.NoteId), cancellationToken);

            note.Publish(_userContext.UserId);

            await _noteRepository.SaveAsync(note, cancellationToken);

            return new MarchNoteResponse();
        }
    }
}