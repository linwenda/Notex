﻿using AutoMapper;
using MediatR;
using SmartNote.Application.Configuration.Commands;
using SmartNote.Application.Configuration.Security.Users;
using SmartNote.Application.Notes.Commands;
using SmartNote.Domain;
using SmartNote.Domain.Notes;
using SmartNote.Domain.Notes.Blocks;
using SmartNote.Domain.Spaces;

namespace SmartNote.Application.Notes.Handlers
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
        private readonly IMapper _mapper;
        private readonly ICurrentUser _currentUser;
        private readonly INoteRepository _noteRepository;
        private readonly IRepository<Space> _spaceRepository;

        public NoteCommandHandler(
            IMapper mapper,
            ICurrentUser currentUser,
            INoteRepository noteRepository,
            IRepository<Space> spaceRepository)
        {
            _mapper = mapper;
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

            note.Update(_currentUser.Id,  _mapper.Map<List<Block>>(request.Blocks));

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
                snapshot.Blocks,
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