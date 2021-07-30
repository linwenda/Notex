using System;
using MarchNote.Application.Configuration.Queries;
using MarchNote.Application.Configuration.Responses;

namespace MarchNote.Application.NoteCooperations.Queries
{
    public class GetNoteCooperationByIdQuery : IQuery<MarchNoteResponse<NoteCooperationDto>>
    {
        public Guid CooperationId { get; }

        public GetNoteCooperationByIdQuery(Guid cooperationId)
        {
            CooperationId = cooperationId;
        }
    }
}