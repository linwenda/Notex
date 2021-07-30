using System.Threading;
using System.Threading.Tasks;
using MarchNote.Application.Configuration.Commands;
using MarchNote.Application.Configuration.Responses;
using MarchNote.Domain.NoteAggregate;
using MarchNote.Domain.Users;

namespace MarchNote.Application.Notes.Commands
{
    public class MergeNoteCommandHandler : ICommandHandler<MergeNoteCommand, MarchNoteResponse>
    {
        private readonly INoteRepository _noteRepository;
        private readonly IUserContext _userContext;

        public MergeNoteCommandHandler(INoteRepository noteRepository, IUserContext userContext)
        {
            _noteRepository = noteRepository;
            _userContext = userContext;
        }

        public async Task<MarchNoteResponse> Handle(MergeNoteCommand request, CancellationToken cancellationToken)
        {
            var note = await _noteRepository.LoadAsync(new NoteId(request.NoteId), cancellationToken);

            note.Merge(_userContext.UserId);
            
            await _noteRepository.SaveAsync(note,cancellationToken);

            return new MarchNoteResponse();
        }
    }
}