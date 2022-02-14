using MediatR;
using SmartNote.Application.Configuration.Commands;
using SmartNote.Application.Configuration.Security.Users;
using SmartNote.Application.Notes.Commands;
using SmartNote.Domain.Notes;
using SmartNote.Domain.Spaces;

namespace SmartNote.Application.Notes.Handlers
{
    public class NoteCommandHandler :
        ICommandHandler<CreateNoteCommand, Guid>,
        ICommandHandler<UpdateNoteCommand, Unit>,
        ICommandHandler<DeleteNoteCommand, Unit>,
        ICommandHandler<ForkNoteCommand, Guid>,
        ICommandHandler<PublishNoteCommand, Unit>
    {
        private readonly ICurrentUser _currentUser;
        private readonly INoteRepository _noteRepository;
        private readonly ISpaceRepository _spaceRepository;

        public NoteCommandHandler(
            ICurrentUser currentUser,
            INoteRepository noteRepository,
            ISpaceRepository spaceRepository)
        {
            _currentUser = currentUser;
            _noteRepository = noteRepository;
            _spaceRepository = spaceRepository;
        }

        public async Task<Guid> Handle(CreateNoteCommand request, CancellationToken cancellationToken)
        {
            var space = await _spaceRepository.GetAsync(request.SpaceId);

            var note = space.CreateNote(
                _currentUser.Id,
                request.Title);

            await _noteRepository.SaveAsync(note);

            return note.Id.Value;
        }

        public async Task<Unit> Handle(UpdateNoteCommand request, CancellationToken cancellationToken)
        {
            var note = await _noteRepository.LoadAsync(new NoteId(request.NoteId));

            note.Update(_currentUser.Id, request.Blocks);

            await _noteRepository.SaveAsync(note);

            return Unit.Value;
        }

        public async Task<Unit> Handle(DeleteNoteCommand request, CancellationToken cancellationToken)
        {
            var note = await _noteRepository.LoadAsync(new NoteId(request.NoteId));

            note.Delete(_currentUser.Id);

            await _noteRepository.SaveAsync(note);

            return Unit.Value;
        }

        public async Task<Guid> Handle(ForkNoteCommand request, CancellationToken cancellationToken)
        {
            var note = await _noteRepository.LoadAsync(new NoteId(request.NoteId));

            var newNote = note.Fork(_currentUser.Id);

            await _noteRepository.SaveAsync(newNote);

            return newNote.Id.Value;
        }

        public async Task<Unit> Handle(PublishNoteCommand request, CancellationToken cancellationToken)
        {
            var note = await _noteRepository.LoadAsync(new NoteId(request.NoteId));

            note.Publish(_currentUser.Id);

            await _noteRepository.SaveAsync(note);

            return Unit.Value;
        }
    }
}