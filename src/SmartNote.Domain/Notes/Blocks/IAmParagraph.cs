namespace SmartNote.Domain.Notes.Blocks;

public interface IAmParagraph : IAmBlockData
{
    string Text { get; set; }
}

public record Paragraph(string Text) : IAmParagraph
{
    public string Text { get; set; } = Text;
}