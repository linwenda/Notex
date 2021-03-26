using System;
using Funzone.BuildingBlocks.Infrastructure.EventBus;

namespace Funzone.Services.Identity.Application.IntegrationEvents.Events
{
    public class UserRegisteredIntegrationEvent : IntegrationEvent
    {
        public Guid UserId { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }

        public UserRegisteredIntegrationEvent()
        {
            
        }

        public UserRegisteredIntegrationEvent(
            Guid userId,
            string userName,
            string email)
        {
            this.UserName = userName;
            this.Email = email;
            this.UserId = userId;
        }
    }
}
