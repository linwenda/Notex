using System;
using System.Collections.Generic;
using MarchNote.Application.Configuration.Queries;
using MarchNote.Domain.Notes.ReadModels;

namespace MarchNote.Application.Notes.Queries
{
    public class GetNotesBySpaceIdQuery : IQuery<IEnumerable<NoteReadModel>>
    {
        public Guid SpaceId { get; }

        public GetNotesBySpaceIdQuery(Guid spaceId)
        {
            SpaceId = spaceId;
        }
    }
}