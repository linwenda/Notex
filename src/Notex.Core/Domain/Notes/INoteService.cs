using Notex.Core.DependencyInjection;

namespace Notex.Core.Domain.Notes;

public interface INoteService : IScopedLifetime
{
    Task RestoreNoteAsync(Note note, Guid historyId, Guid userId);
}