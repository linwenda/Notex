namespace SmartNote.Core.Shared;

public interface IClock
{
    DateTime Now { get; }
}