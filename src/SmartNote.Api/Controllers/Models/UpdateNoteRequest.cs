using System.Text.Json;
using Newtonsoft.Json.Linq;
using SmartNote.Application.Notes.Queries;

namespace SmartNote.Api.Controllers.Models;

public class UpdateNoteRequest
{
    public string Title { get; set; }
    public List<BlockRequest> Blocks { get; set; }
}

public class BlockRequest
{
    public string Id { get; set; }
    public string Type { get; set; }
    public JsonElement Data { get; set; }
}