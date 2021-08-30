using System;
using System.Threading;
using System.Threading.Tasks;
using MarchNote.Application.Configuration.Commands;
using MarchNote.Application.Configuration.Extensions;
using MarchNote.Application.Configuration.Responses;
using MarchNote.Application.Notes.Commands;
using MarchNote.Domain.Notes;
using MarchNote.Domain.SeedWork;
using MarchNote.Domain.Spaces;
using MarchNote.Domain.Users;

namespace MarchNote.Application.Notes.Handlers
{
    public class NoteCommandHandler : 
        ICommandHandler<CreateNoteCommand, MarchNoteResponse<Guid>>,
        ICommandHandler<EditNoteCommand, MarchNoteResponse>,
        ICommandHandler<DeleteNoteCommand, MarchNoteResponse>,
        ICommandHandler<DraftOutNoteCommand, MarchNoteResponse<Guid>>,
        ICommandHandler<MergeNoteCommand, MarchNoteResponse>,
        ICommandHandler<PublishNoteCommand, MarchNoteResponse>,
        ICommandHandler<RemoveNoteMemberCommand, MarchNoteResponse>
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
        
        public async Task<MarchNoteResponse<Guid>> Handle(CreateNoteCommand request, CancellationToken cancellationToken)
        {
            var space = await _spaceRepository.FindAsync(new SpaceId(request.SpaceId));

            var note = Note.Create(
                space,
                _userContext.UserId,
                request.Title,
                request.Content,
                request.Tags);

            await _noteRepository.SaveAsync(note);

            return new MarchNoteResponse<Guid>(note.Id.Value);
        }

        public async Task<MarchNoteResponse> Handle(EditNoteCommand request, CancellationToken cancellationToken)
        {
            var note = await _noteRepository.LoadAsync(new NoteId(request.NoteId));

            note.Edit(
                _userContext.UserId,
                request.Title,
                request.Content);

            await _noteRepository.SaveAsync(note);

            return new MarchNoteResponse();
        }

        public async Task<MarchNoteResponse> Handle(DeleteNoteCommand request, CancellationToken cancellationToken)
        {
            var note = await _noteRepository.LoadAsync(new NoteId(request.NoteId));

            note.Delete(_userContext.UserId);

            await _noteRepository.SaveAsync(note);

            return new MarchNoteResponse();
        }

        public async Task<MarchNoteResponse<Guid>> Handle(DraftOutNoteCommand request, CancellationToken cancellationToken)
        {
            var note = await _noteRepository.LoadAsync(new NoteId(request.NoteId));

            var newNote = note.DraftOut(_userContext.UserId);

            await _noteRepository.SaveAsync(newNote);

            return new MarchNoteResponse<Guid>(newNote.Id.Value);
        }

        public async Task<MarchNoteResponse> Handle(MergeNoteCommand request, CancellationToken cancellationToken)
        {
            var note = await _noteRepository.LoadAsync(new NoteId(request.NoteId));

            note.Merge(_userContext.UserId);
            
            await _noteRepository.SaveAsync(note);

            return new MarchNoteResponse();
        }

        public async Task<MarchNoteResponse> Handle(PublishNoteCommand request, CancellationToken cancellationToken)
        {
            var note = await _noteRepository.LoadAsync(new NoteId(request.NoteId));

            note.Publish(_userContext.UserId);

            await _noteRepository.SaveAsync(note);

            return new MarchNoteResponse();
        }

        public async Task<MarchNoteResponse> Handle(RemoveNoteMemberCommand request, CancellationToken cancellationToken)
        {
            var note = await _noteRepository.LoadAsync(new NoteId(request.NoteId));

            note.RemoveMember(_userContext.UserId, new UserId(request.UserId));

            await _noteRepository.SaveAsync(note);

            return new MarchNoteResponse();
        }
    }
}