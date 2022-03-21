using Microsoft.EntityFrameworkCore;
using Notex.Core.Aggregates;
using Notex.Core.Aggregates.Notes.ReadModels;
using Notex.Core.Lifetimes;

namespace Notex.Core.Queries;

public class NoteQuery : INoteQuery, IScopedLifetime
{
    private readonly IReadModelRepository _readModelRepository;

    public NoteQuery(IReadModelRepository readModelRepository)
    {
        _readModelRepository = readModelRepository;
    }

    public async Task<NoteDetail> GetNoteAsync(Guid id)
    {
        return await _readModelRepository.Query<NoteDetail>().FirstOrDefaultAsync(n => n.Id == id);
    }

    public async Task<IEnumerable<NoteDetail>> GetNotesFromSpaceAsync(Guid spaceId)
    {
        return await _readModelRepository.Query<NoteDetail>().Where(n => n.SpaceId == spaceId).ToListAsync();
    }

    public async Task<IEnumerable<NoteHistory>> GetHistoriesAsync(Guid id)
    {
        return await _readModelRepository.Query<NoteHistory>().Where(n => n.NoteId == id).ToListAsync();
    }
}