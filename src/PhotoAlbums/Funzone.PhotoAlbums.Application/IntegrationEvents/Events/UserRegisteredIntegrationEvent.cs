using Funzone.BuildingBlocks.Infrastructure.EventBus;
using System;

namespace Funzone.PhotoAlbums.Application.IntegrationEvents.Events
{
    public class UserRegisteredIntegrationEvent : IntegrationEvent
    {
        public Guid UserId { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }

        public UserRegisteredIntegrationEvent() : base(Guid.NewGuid(), DateTime.UtcNow)
        {
        }

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