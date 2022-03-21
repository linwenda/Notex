namespace Notex.Core.Aggregates;

public interface IMementoOriginator
{
    IMemento GetMemento();
    void SetMemento(IMemento memento);
}