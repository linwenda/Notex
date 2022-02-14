using SmartNote.Core.Ddd;

namespace SmartNote.Domain.NoteComments;

public interface INoteCommentRepository : IRepository<NoteComment, Guid>
{
}