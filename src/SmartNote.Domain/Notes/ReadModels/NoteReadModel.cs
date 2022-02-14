using SmartNote.Core.Ddd;
using SmartNote.Core.Extensions;
using SmartNote.Domain.Notes.Blocks;

namespace SmartNote.Domain.Notes.ReadModels
{
    public class NoteReadModel : IEntity, IHasCreationTime
    {
        public NoteReadModel()
        {
            Content = new List<Block>();
        }

        public Guid Id { get; set; }
        public DateTime CreationTime { get; set; }
        public Guid? ForkId { get; set; }
        public Guid AuthorId { get; set; }
        public Guid SpaceId { get; set; }
        public string Title { get; set; }
        public int Version { get; set; }
        public NoteStatus Status { get; set; }
        public bool IsDeleted { get; set; }
        public List<Block> Content { get; set; }

        public string SerializeContent
        {
            get => Content.ToJson();
            set => Content = value.FromJson<List<Block>>();
        }

        public object[] GetKeys()
        {
            return new object[] { Id };
        }
    }
}