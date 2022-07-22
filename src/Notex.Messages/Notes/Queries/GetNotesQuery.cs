namespace Notex.Messages.Notes.Queries;

public class GetNotesQuery : IQuery<GetNotesResponse>
{
    public Guid SpaceId { get; set; }
    public int PageIndex { get; set; } = 1;
    public int PageSize { get; set; } = 15;
}

public class GetNotesResponse
{
    public int PageIndex { get; set; }
    public int TotalCount { get; set; }
    public IEnumerable<NoteDto> Data { get; set; }
}