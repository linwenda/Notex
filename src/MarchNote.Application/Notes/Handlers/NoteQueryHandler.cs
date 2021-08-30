﻿using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MarchNote.Application.Configuration.Queries;
using MarchNote.Application.Configuration.Responses;
using MarchNote.Application.Notes.Queries;
using MarchNote.Domain.Notes.ReadModels;
using MarchNote.Domain.SeedWork;
using MarchNote.Domain.Users;

namespace MarchNote.Application.Notes.Handlers
{
    public class NoteQueryHandler :
        IQueryHandler<GetNoteQuery, MarchNoteResponse<NoteReadModel>>,
        IQueryHandler<GetNotesQuery, MarchNoteResponse<IEnumerable<NoteReadModel>>>,
        IQueryHandler<GetNoteHistoriesQuery, MarchNoteResponse<IEnumerable<NoteHistoryReadModel>>>,
        IQueryHandler<GetNotesBySpaceIdQuery, MarchNoteResponse<IEnumerable<NoteReadModel>>>
    {
        private readonly IUserContext _userContext;
        private readonly IRepository<NoteReadModel> _noteRepository;
        private readonly IRepository<NoteHistoryReadModel> _noteHistoryRepository;

        public NoteQueryHandler(
            IUserContext userContext,
            IRepository<NoteReadModel> noteRepository,
            IRepository<NoteHistoryReadModel> noteHistoryRepository)
        {
            _userContext = userContext;
            _noteRepository = noteRepository;
            _noteHistoryRepository = noteHistoryRepository;
        }

        public async Task<MarchNoteResponse<NoteReadModel>> Handle(GetNoteQuery request,
            CancellationToken cancellationToken)
        {
            return new MarchNoteResponse<NoteReadModel>(
                await _noteRepository.FirstOrDefaultAsync(n => n.Id == request.NoteId));
        }

        public async Task<MarchNoteResponse<IEnumerable<NoteReadModel>>> Handle(GetNotesQuery request,
            CancellationToken cancellationToken)
        {
            return new MarchNoteResponse<IEnumerable<NoteReadModel>>(
                await _noteRepository.ListAsync(n => n.AuthorId == _userContext.UserId.Value));
        }

        public async Task<MarchNoteResponse<IEnumerable<NoteHistoryReadModel>>> Handle(GetNoteHistoriesQuery request,
            CancellationToken cancellationToken)
        {
            return new MarchNoteResponse<IEnumerable<NoteHistoryReadModel>>(await _noteHistoryRepository.ListAsync(
                n => n.NoteId == request.NoteId));
        }

        public async Task<MarchNoteResponse<IEnumerable<NoteReadModel>>> Handle(GetNotesBySpaceIdQuery request,
            CancellationToken cancellationToken)
        {
            return new MarchNoteResponse<IEnumerable<NoteReadModel>>(
                await _noteRepository.ListAsync(n => n.SpaceId == request.SpaceId));
        }
    }
}