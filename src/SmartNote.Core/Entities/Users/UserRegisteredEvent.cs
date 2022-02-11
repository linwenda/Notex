namespace SmartNote.Core.Entities.Users;

public class UserRegisteredEvent : IDomainEvent
{
    public string Email { get; }

    public UserRegisteredEvent(string email)
    {
        Email = email;
    }

    public Guid Id { get; }
    
    public DateTime OccurredTime { get; }
}