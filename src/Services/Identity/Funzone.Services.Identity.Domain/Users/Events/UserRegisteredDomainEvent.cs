using Funzone.BuildingBlocks.Domain;

namespace Funzone.Services.Identity.Domain.Users.Events
{
    public class UserRegisteredDomainEvent : DomainEventBase
    {
        public UserRegisteredDomainEvent(UserId userId, string userName, string email, string nickname)
        {
            UserId = userId;
            UserName = userName;
            Email = email;
            Nickname = nickname;
        }

        public UserId UserId { get; }
        public string UserName { get; }
        public string Email { get; }
        public string Nickname { get; }
    }
}
