using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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
    
    public static Block Of(string id, string type, string data)
    {
        IAmBlockData blockData = null;

        var blockType = BlockType.Of(type);

        if (blockType == BlockType.Delimiter)
        {
            blockData = JsonConvert.DeserializeObject<Delimiter>(data);
        }

        if (blockType == BlockType.Header)
        {
            blockData = JsonConvert.DeserializeObject<Header>(data);
        }

        if (blockType == BlockType.Image)
        {
            blockData = JsonConvert.DeserializeObject<Image>(data);
        }

        if (blockType == BlockType.List)
        {
            blockData = JsonConvert.DeserializeObject<List>(data);
        }

        if (blockType == BlockType.Paragraph)
        {
            blockData = JsonConvert.DeserializeObject<IAmParagraph>(data);
        }

        if (blockData == null)
        {
            throw new ArgumentNullException(nameof(blockData));
        }

        return new Block(id, blockType, blockData);
    }

    public static Block Of(string id, string type, JObject data)
    {
        IAmBlockData blockData = null;

        var blockType = BlockType.Of(type);

        if (blockType == BlockType.Delimiter)
        {
            blockData = data.ToObject<IAmDelimiter>();
        }
        else if (blockType == BlockType.Header)
        {
            blockData = data.ToObject<IAmHeader>();
        }
        else if (blockType == BlockType.Image)
        {
            blockData = data.ToObject<IAmImage>();
        }
        else if (blockType == BlockType.List)
        {
            blockData = data.ToObject<IAmList>();
        }
        else if (blockType == BlockType.Paragraph)
        {
            blockData = data.ToObject<IAmParagraph>();
        }

        if (blockData == null)
        {
            throw new ArgumentNullException(nameof(blockData));
        }

        return new Block(id, blockType, blockData);
    }
}