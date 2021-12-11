namespace SmartNote.Core.Application.NoteCooperations.Contracts
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