namespace SmartNote.Domain.Notes.Blocks;

public record BlockType
{
    public static BlockType Header => new BlockType(nameof(Header).ToLower());
    public static BlockType Paragraph => new BlockType(nameof(Paragraph).ToLower());
    public static BlockType Delimiter => new BlockType(nameof(Delimiter).ToLower());
    public static BlockType Image => new BlockType(nameof(Image).ToLower());
    public static BlockType List => new BlockType(nameof(List).ToLower());
    public string Value { get; }

    private BlockType(string value)
    {
        Value = value;
    }

    public static BlockType Of(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            throw new ArgumentNullException(value);
        }
        
        var blockType = new BlockType(value.ToLower());

        if (!SupportedBlockTypes.Contains(blockType))
        {
            throw new ArgumentException($"{value} types unsupported");
        }

        return blockType;
    }

    private static IEnumerable<BlockType> SupportedBlockTypes
    {
        get
        {
            yield return Header;
            yield return Paragraph;
            yield return Delimiter;
            yield return Image;
            yield return List;
        }
    }

    public override string ToString()
    {
        return Value;
    }
}