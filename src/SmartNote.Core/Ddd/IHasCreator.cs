namespace SmartNote.Core.Ddd;

public interface IHasCreator
{
    Guid CreatorId { get; }
}