using SmartNote.Core.Domain.Notes.Blocks;
using SmartNote.Core.Extensions;

namespace SmartNote.Core.Domain.Notes.ReadModels
{
    public class NoteReadModel : Entity<Guid>, IHasCreationTime
    {
        public NoteReadModel()
        {
            Blocks = new List<Block>();
        }

        public DateTime CreationTime { get; set; }
        public Guid? ForkId { get; set; }
        public Guid AuthorId { get; set; }
        public Guid SpaceId { get; set; }
        public string Title { get; set; }
        public int Version { get; set; }
        public NoteStatus Status { get; set; }
        public bool IsDeleted { get; set; }
        public List<Block> Blocks { get; set; }

        public string SerializeBlocks
        {
            get => Blocks.ToJson();
            set => Blocks = value.FromJson<List<Block>>();
        }
    }
}