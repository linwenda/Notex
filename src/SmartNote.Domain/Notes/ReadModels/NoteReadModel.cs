using Newtonsoft.Json;

namespace SmartNote.Domain.Notes.ReadModels
{
    public class NoteReadModel : IReadModelEntity, IHasCreationTime
    {
        public NoteReadModel()
        {
            Blocks = new List<BlockReadModel>();
        }

        public Guid Id { get; set; }
        public DateTimeOffset CreationTime { get; set; }
        public Guid? ForkId { get; set; }
        public Guid AuthorId { get; set; }
        public Guid SpaceId { get; set; }
        public string Title { get; set; }
        public int Version { get; set; }
        public NoteStatus Status { get; set; }
        public bool IsDeleted { get; set; }
        public List<BlockReadModel> Blocks { get; set; }
        public string SerializeBlocks
        {
            get => JsonConvert.SerializeObject(Blocks);
            set => Blocks = JsonConvert.DeserializeObject<List<BlockReadModel>>(value);
        }
    }

    public class BlockReadModel
    {
        public string Id { get; set; }
        public string Type { get; set; }
        public string Data { get; set; }
    }
}