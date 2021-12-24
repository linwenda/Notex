namespace SmartNote.Domain.Notes.Blocks;

public interface IAmList : IAmBlockData
{
    string Style { get; set; }
    List<string> Items { get; set; }
}

public class List : IAmList
{
    public string Style { get; set; }
    public List<string> Items { get; set; }
}