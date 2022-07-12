namespace Notex.Messages.Notes.Queries;

public class GetSpaceNotesQuery : IQuery<IEnumerable<NoteDto>>
{
    public Guid SpaceId { get; }

    public GetSpaceNotesQuery(Guid spaceId)
    {
        SpaceId = spaceId;
    }
}