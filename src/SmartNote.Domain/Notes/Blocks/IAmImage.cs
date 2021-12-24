namespace SmartNote.Domain.Notes.Blocks;

public interface IAmImage : IAmBlockData
{
    List<ImageFile> File { get; set; }
    bool WithBorder { get; set; }
    bool WithBackground { get; set; }
    bool Stretched { get; set; }
}

public class ImageFile
{
    public string Url { get; set; }
}

public class Image : IAmImage
{
    public List<ImageFile> File { get; set; }
    public bool WithBorder { get; set; }
    public bool WithBackground { get; set; }
    public bool Stretched { get; set; }
}