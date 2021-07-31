using System;
using System.Threading;
using System.Threading.Tasks;
using MarchNote.Application.Configuration.Commands;
using MarchNote.Application.Configuration.Extensions;
using MarchNote.Application.Configuration.Responses;
using MarchNote.Domain.NoteAggregate;
using MarchNote.Domain.SeedWork;
using MarchNote.Domain.Spaces;
using MarchNote.Domain.Users;

namespace MarchNote.Application.Notes.Commands
{
    public class CreateNoteCommandHandler : ICommandHandler<CreateNoteCommand,MarchNoteResponse<Guid>>
    {
        private readonly IUserContext _userContext;
        private readonly INoteRepository _noteRepository;
        private readonly IRepository<Space> _spaceRepository;

        public CreateNoteCommandHandler(
            IUserContext userContext,
            INoteRepository noteRepository,
            IRepository<Space> spaceRepository)
        {
            _userContext = userContext;
            _noteRepository = noteRepository;
            _spaceRepository = spaceRepository;
        }

        public async Task<MarchNoteResponse<Guid>> Handle(CreateNoteCommand request, CancellationToken cancellationToken)
        {
            var space = await _spaceRepository.FindAsync(new SpaceId(request.SpaceId));

            var note = new Note(new NoteId(Guid.NewGuid()));

            note.Create(
                space,
                _userContext.UserId, 
                request.Title, 
                request.Content);

            await _noteRepository.SaveAsync(note, cancellationToken);

            return new MarchNoteResponse<Guid>(note.Id.Value);
        }
    }
}