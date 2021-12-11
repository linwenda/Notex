﻿using SmartNote.Core.Application.Notes.Contracts;
using SmartNote.Core.Domain;
using SmartNote.Core.Domain.Notes.ReadModels;
using SmartNote.Core.Security.Users;

namespace SmartNote.Core.Application.Notes.Handlers
{
    public class NoteQueryHandler :
        IQueryHandler<GetNoteQuery, NoteReadModel>,
        IQueryHandler<GetNotesQuery, IEnumerable<NoteReadModel>>,
        IQueryHandler<GetNoteHistoriesQuery, IEnumerable<NoteHistoryReadModel>>,
        IQueryHandler<GetNotesBySpaceIdQuery, IEnumerable<NoteReadModel>>
    {
        private readonly ICurrentUser _currentUser;
        private readonly IRepository<NoteReadModel> _noteRepository;
        private readonly IRepository<NoteHistoryReadModel> _noteHistoryRepository;

        public NoteQueryHandler(
            ICurrentUser currentUser,
            IRepository<NoteReadModel> noteRepository,
            IRepository<NoteHistoryReadModel> noteHistoryRepository)
        {
            _currentUser = currentUser;
            _noteRepository = noteRepository;
            _noteHistoryRepository = noteHistoryRepository;
        }

        public async Task<NoteReadModel> Handle(GetNoteQuery request,
            CancellationToken cancellationToken)
        {
            return await _noteRepository.FirstOrDefaultAsync(n => n.Id == request.NoteId);
        }

        public async Task<IEnumerable<NoteReadModel>> Handle(GetNotesQuery request,
            CancellationToken cancellationToken)
        {
            return await _noteRepository.ListAsync(n => n.AuthorId == _currentUser.Id);
        }

        public async Task<IEnumerable<NoteHistoryReadModel>> Handle(GetNoteHistoriesQuery request,
            CancellationToken cancellationToken)
        {
            return await _noteHistoryRepository.ListAsync(
                n => n.NoteId == request.NoteId);
        }

        public async Task<IEnumerable<NoteReadModel>> Handle(GetNotesBySpaceIdQuery request,
            CancellationToken cancellationToken)
        {
            return await _noteRepository.ListAsync(n => n.SpaceId == request.SpaceId);
        }
    }
}