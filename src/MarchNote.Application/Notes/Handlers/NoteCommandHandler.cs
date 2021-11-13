using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MarchNote.Application.Configuration.Commands;
using MarchNote.Application.Configuration.Extensions;
using MarchNote.Application.Notes.Commands;
using MarchNote.Domain.Notes;
using MarchNote.Domain.Shared;
using MarchNote.Domain.Spaces;
using MarchNote.Domain.Users;
using MediatR;

namespace MarchNote.Application.Notes.Handlers
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
            var space = await _spaceRepository.CheckNotNull(request.SpaceId);

            var note = space.CreateNote(
                _userContext.UserId,
                request.Title,
                request.Content,
                request.Tags);

            await _noteRepository.SaveAsync(note);

            return note.Id.Value;
        }

        public async Task<Unit> Handle(UpdateNoteCommand request, CancellationToken cancellationToken)
        {
            var note = await _noteRepository.LoadAsync(new NoteId(request.NoteId));

            note.Update(_userContext.UserId, request.Title, request.Content, request.Tags);

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

        public async Task<Guid> Handle(ForkNoteCommand request, CancellationToken cancellationToken)
        {
            var note = await _noteRepository.LoadAsync(new NoteId(request.NoteId));

            var newNote = note.Fork(_userContext.UserId);

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
                _userContext.UserId,
                snapshot.Title,
                snapshot.Content, 
                new List<string>());

            await _noteRepository.SaveAsync(forkNote);

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