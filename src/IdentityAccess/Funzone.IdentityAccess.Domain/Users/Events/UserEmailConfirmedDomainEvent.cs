using Funzone.BuildingBlocks.Domain;

namespace Funzone.IdentityAccess.Domain.Users.Events
{
    public class UserEmailConfirmedDomainEvent : DomainEventBase
    {
        public UserEmailConfirmedDomainEvent(UserId userId, string email)
        {
            UserId = userId;
            Email = email;
        }

        public UserId UserId { get; }
        public string Email { get; }
    }
}
