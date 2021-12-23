using SmartNote.Core.Domain.Notes.ReadModels;

namespace SmartNote.Core.Application.Notes.Queries
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