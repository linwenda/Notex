using Funzone.BuildingBlocks.Infrastructure.EventBus;
using System;

namespace Funzone.Services.Albums.Application.IntegrationEvents.Events
{
    public class UserRegisteredIntegrationEvent : IntegrationEvent
    {
        public Guid UserId { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }

        public UserRegisteredIntegrationEvent()
        {
        }

        public UserRegisteredIntegrationEvent(Guid userId, string email, string userName)
        {
            UserId = userId;
            Email = email;
            UserName = userName;
        }
    }
}