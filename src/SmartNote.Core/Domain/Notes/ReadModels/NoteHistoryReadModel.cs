using SmartNote.Core.Domain.Notes.Blocks;

namespace SmartNote.Core.Domain.Notes.ReadModels
{
    public class NoteHistoryReadModel : ReadModelEntity<Guid>, IHasCreationTime
    {
        public Guid NoteId { get; set; }
        public Guid AuthorId { get; set; }
        public DateTime CreationTime { get; set; }
        public string Title { get; set; }
        public List<Block> Blocks { get; set; }
        public int Version { get; set; }
        public string Comment { get; set; }
    }
}