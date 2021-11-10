using System;
using System.Threading;
using System.Threading.Tasks;
using MarchNote.Application.Configuration.Commands;
using MarchNote.Application.Notes.Commands;
using MarchNote.Domain.Notes;
using MarchNote.Domain.SeedWork;
using MarchNote.Domain.Spaces;
using MarchNote.Domain.Users;
using MediatR;

namespace MarchNote.Application.Notes.Handlers
{
    public class NoteCommandHandler :
        ICommandHandler<CreateNoteCommand, Guid>,
        ICommandHandler<EditNoteCommand, Unit>,
        ICommandHandler<DeleteNoteCommand, Unit>,
        ICommandHandler<DraftOutNoteCommand, Guid>,
        ICommandHandler<MergeNoteCommand, Unit>,
        ICommandHandler<PublishNoteCommand, Unit>,
        ICommandHandler<RemoveNoteMemberCommand, Unit>
    {
        private readonly IUserContext _userContext;
        private readonly INoteRepository _noteRepository;
        private readonly IRepository<Space> _spaceRepository;

        public NoteCommandHandler(
            IUserContext userContext,
            INoteRepository noteRepository,
            IRepository<Space> spaceRepository)
        {
            _userContext = userContext;
            _noteRepository = noteRepository;
            _spaceRepository = spaceRepository;
        }

        public async Task<Guid> Handle(CreateNoteCommand request, CancellationToken cancellationToken)
        {
            var space = await _spaceRepository.GetByIdAsync(request.SpaceId);

            var note = space.CreateNote(
                _userContext.UserId, 
                request.Title, 
                request.Content, 
                request.Tags);

            await _noteRepository.SaveAsync(note);

            return note.Id.Value;
        }

        public async Task<Unit> Handle(EditNoteCommand request, CancellationToken cancellationToken)
        {
            var note = await _noteRepository.LoadAsync(new NoteId(request.NoteId));

            note.Edit(
                _userContext.UserId,
                request.Title,
                request.Content);

            await _noteRepository.SaveAsync(note);

            return Unit.Value;
        }

        public async Task<Unit> Handle(DeleteNoteCommand request, CancellationToken cancellationToken)
        {
            var note = await _noteRepository.LoadAsync(new NoteId(request.NoteId));

            note.Delete(_userContext.UserId);

            await _noteRepository.SaveAsync(note);

            return Unit.Value;
        }

        public async Task<Guid> Handle(DraftOutNoteCommand request, CancellationToken cancellationToken)
        {
            var note = await _noteRepository.LoadAsync(new NoteId(request.NoteId));

            var newNote = note.DraftOut(_userContext.UserId);

            await _noteRepository.SaveAsync(newNote);

            return newNote.Id.Value;
        }

        public async Task<Unit> Handle(MergeNoteCommand request, CancellationToken cancellationToken)
        {
            var note = await _noteRepository.LoadAsync(new NoteId(request.NoteId));

            note.Merge(_userContext.UserId);

            await _noteRepository.SaveAsync(note);

            return Unit.Value;
        }

        public async Task<Unit> Handle(PublishNoteCommand request, CancellationToken cancellationToken)
        {
            var note = await _noteRepository.LoadAsync(new NoteId(request.NoteId));

            note.Publish(_userContext.UserId);

            await _noteRepository.SaveAsync(note);

            return Unit.Value;
        }

        public async Task<Unit> Handle(RemoveNoteMemberCommand request, CancellationToken cancellationToken)
        {
            var note = await _noteRepository.LoadAsync(new NoteId(request.NoteId));

            note.RemoveMember(_userContext.UserId, request.UserId);

            await _noteRepository.SaveAsync(note);

            return Unit.Value;
        }
    }
}