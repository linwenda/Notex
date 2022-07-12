namespace Notex.Api.Controllers.Notes;

public class EditNoteRequest
{
    public string Title { get; set; }
    public string Content { get; set; }
    public string Comment { get; set; }
}