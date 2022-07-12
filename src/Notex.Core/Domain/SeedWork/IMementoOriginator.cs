namespace Notex.Core.Domain.SeedWork;

public interface IMementoOriginator
{
    IMemento GetMemento();
    void SetMemento(IMemento memento);
}