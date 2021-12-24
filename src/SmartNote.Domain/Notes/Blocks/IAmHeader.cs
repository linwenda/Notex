namespace SmartNote.Domain.Notes.Blocks;

public interface IAmHeader : IAmBlockData
{
    string Text { get; set; }
    int Level { get; set; }
}

public record Header(string Text, int Level) : IAmHeader
{
    public string Text { get; set; } = Text;
    public int Level { get; set; } = Level;
}