using MediatR;
using SmartNote.Core.Application.Notes.Contracts;
using SmartNote.Core.Domain;
using SmartNote.Core.Domain.Notes;
using SmartNote.Core.Domain.Spaces;
using SmartNote.Core.Security.Users;

namespace SmartNote.Core.Application.Notes.Handlers
{
    public class NoteCommandHandler :
        ICommandHandler<CreateNoteCommand, Guid>,
        ICommandHandler<UpdateNoteCommand, Unit>,
        ICommandHandler<DeleteNoteCommand, Unit>,
        ICommandHandler<ForkNoteCommand, Guid>,
        ICommandHandler<MergeNoteCommand, Unit>,
        ICommandHandler<PublishNoteCommand, Unit>,
        ICommandHandler<RemoveNoteMemberCommand, Unit>
    {
        private readonly ICurrentUser _currentUser;
        private readonly INoteRepository _noteRepository;
        private readonly IRepository<Space> _spaceRepository;

        public NoteCommandHandler(
            ICurrentUser currentUser,
            INoteRepository noteRepository,
            IRepository<Space> spaceRepository)
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
                request.Title,
                request.Content,
                request.Tags);

            await _noteRepository.SaveAsync(note);

            return note.Id.Value;
        }

        public async Task<Unit> Handle(UpdateNoteCommand request, CancellationToken cancellationToken)
        {
            var note = await _noteRepository.LoadAsync(new NoteId(request.NoteId));

            note.Update(_currentUser.Id, request.Title, request.Content, request.Tags);

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

        public async Task<Unit> Handle(MergeNoteCommand request, CancellationToken cancellationToken)
        {
            var note = await _noteRepository.LoadAsync(new NoteId(request.NoteId));

            var snapshot = note.GetSnapshot();

            var forkNote = await _noteRepository.LoadAsync(new NoteId(request.ForkId));

            forkNote.Merge(
                note.Id.Value,
                _currentUser.Id,
                snapshot.Title,
                snapshot.Content,
                new List<string>());

            await _noteRepository.SaveAsync(forkNote);

            return Unit.Value;
        }

        public async Task<Unit> Handle(PublishNoteCommand request, CancellationToken cancellationToken)
        {
            var note = await _noteRepository.LoadAsync(new NoteId(request.NoteId));

            note.Publish(_currentUser.Id);

            await _noteRepository.SaveAsync(note);

            return Unit.Value;
        }

        public async Task<Unit> Handle(RemoveNoteMemberCommand request, CancellationToken cancellationToken)
        {
            var note = await _noteRepository.LoadAsync(new NoteId(request.NoteId));

            note.RemoveMember(_currentUser.Id, request.UserId);

            await _noteRepository.SaveAsync(note);

            return Unit.Value;
        }
    }
}