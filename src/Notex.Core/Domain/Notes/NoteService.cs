using Notex.Core.Domain.Notes.ReadModels;
using Notex.Core.Domain.SeedWork;

namespace Notex.Core.Domain.Notes;

public class NoteService : INoteService
{
    private readonly IReadOnlyRepository<NoteHistory> _historyRepository;

    public NoteService(
        IReadOnlyRepository<NoteHistory> historyRepository)
    {
        _historyRepository = historyRepository;
    }

    public async Task RestoreNoteAsync(Note note, Guid historyId, Guid userId)
    {
        var history = await _historyRepository.GetAsync(historyId);

        note.Restore(userId, history.Title, history.Content, history.Version);
    }
}