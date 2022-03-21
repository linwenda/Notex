using Notex.Core.Aggregates.Notes.ReadModels;
using Notex.Core.Lifetimes;
using Notex.Messages.Notes;

namespace Notex.Core.Aggregates.Notes.DomainServices;

public class NoteChecker : INoteChecker, IScopedLifetime
{
    private readonly IReadModelRepository _readModelRepository;

    public NoteChecker(IReadModelRepository readModelRepository)
    {
        _readModelRepository = readModelRepository;
    }

    public bool IsPublishedNote(Guid noteId)
    {
        return _readModelRepository.Query<NoteDetail>().Any(n => n.Status == NoteStatus.Published);
    }
}