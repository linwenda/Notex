namespace SmartNote.Core.Entities;

public interface IHasCreator
{
    Guid CreatorId { get; }
}