namespace SmartNote.Domain.Notes.Blocks;

public class BlockHeader : IAmBlockData
{
    public string Text { get; set; }
    public int Level { get; set; }
}