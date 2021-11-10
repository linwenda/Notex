using System;
using MarchNote.Application.Configuration.Queries;

namespace MarchNote.Application.NoteCooperations.Queries
{
    public class GetNoteCooperationByIdQuery : IQuery<NoteCooperationDto>
    {
        public Guid CooperationId { get; }

        public GetNoteCooperationByIdQuery(Guid cooperationId)
        {
            CooperationId = cooperationId;
        }
    }
}