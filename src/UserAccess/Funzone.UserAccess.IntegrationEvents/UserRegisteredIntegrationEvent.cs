using Funzone.BuildingBlocks.Infrastructure.EventBus;
using System;

namespace Funzone.UserAccess.IntegrationEvents
{
    public class UserRegisteredIntegrationEvent : IntegrationEvent
    {
        public Guid UserId { get; }
        public string Email { get; }
        public string UserName { get; }

        public UserRegisteredIntegrationEvent(
            Guid id,
            DateTime occurredOn,
            Guid userId,
            string userName,
            string email)
            : base(id, occurredOn)
        {
            this.UserName = userName;
            this.Email = email;
            this.UserId = userId;
        }
    }
}