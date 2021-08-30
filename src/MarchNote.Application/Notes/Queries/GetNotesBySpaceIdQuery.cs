using System;
using System.Collections.Generic;
using MarchNote.Application.Configuration.Queries;
using MarchNote.Application.Configuration.Responses;
using MarchNote.Domain.Notes.ReadModels;

namespace MarchNote.Application.Notes.Queries
{
    public class GetNotesBySpaceIdQuery : IQuery<MarchNoteResponse<IEnumerable<NoteReadModel>>>
    {
        public Guid SpaceId { get; }

        public GetNotesBySpaceIdQuery(Guid spaceId)
        {
            SpaceId = spaceId;
        }
    }
}