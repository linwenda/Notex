using SmartNote.Application.Configuration.Queries;

namespace SmartNote.Application.NoteCooperations.Queries
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