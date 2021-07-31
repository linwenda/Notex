﻿using System;
using System.Collections.Generic;
using MarchNote.Application.Configuration.Queries;
using MarchNote.Application.Configuration.Responses;

namespace MarchNote.Application.Spaces.Queries
{
    public class GetSpaceFoldersQuery : IQuery<MarchNoteResponse<IEnumerable<SpaceFolderDto>>>
    {
        public Guid SpaceId { get; }

        public GetSpaceFoldersQuery(Guid spaceId)
        {
            SpaceId = spaceId;
        }
    }
}