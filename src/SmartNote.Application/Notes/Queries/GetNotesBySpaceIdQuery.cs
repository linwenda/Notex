﻿using SmartNote.Application.Configuration.Queries;
using SmartNote.Domain.Notes.ReadModels;

namespace SmartNote.Application.Notes.Queries
{
    public class GetNotesBySpaceIdQuery : IQuery<IEnumerable<NoteSimpleDto>>
    {
        public Guid SpaceId { get; }

        public GetNotesBySpaceIdQuery(Guid spaceId)
        {
            SpaceId = spaceId;
        }
    }
}