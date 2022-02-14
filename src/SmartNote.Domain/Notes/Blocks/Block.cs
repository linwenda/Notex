namespace SmartNote.Domain.Notes.Blocks;

public record Block
{
    private Block()
    {
    }

    public Block(string id, string type, string data)
    {
        Id = id;
        Type = type;
        Data = data;
    }

    public string Id { get; set; }
    public string Type { get; set; }
    public string Data { get; set; }
}