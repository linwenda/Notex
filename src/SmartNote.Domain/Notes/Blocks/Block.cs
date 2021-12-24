namespace SmartNote.Domain.Notes.Blocks;

public record Block
{
    public Block()
    {
    }

    public Block(string id, BlockType type, IAmBlockData data)
    {
        Id = id;
        Type = type;
        Data = data ?? throw new ArgumentNullException(nameof(data));
    }

    public string Id { get; set; }
    public BlockType Type { get; set; }
    public IAmBlockData Data { get; set; }
}