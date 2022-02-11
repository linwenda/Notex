namespace SmartNote.Core.Entities.Users;

public class User : AggregateRoot<Guid>, IHasCreator
{
    public Guid CreatorId { get; set; }
}