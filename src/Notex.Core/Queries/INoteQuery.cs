using Notex.Core.Aggregates.Notes.ReadModels;

namespace Notex.Core.Queries;

public interface INoteQuery
{
    Task<NoteDetail> GetNoteAsync(Guid id);
    Task<IEnumerable<NoteDetail>> GetNotesFromSpaceAsync(Guid spaceId);
    Task<IEnumerable<NoteHistory>> GetHistoriesAsync(Guid id);
}