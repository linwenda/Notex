namespace Funzone.BuildingBlocks.EventBus.MassTransit
{
    public class MassTransitEventBusSettings
    {
        public MassTransitEventBusSettings(
            string hostAddress, 
            string userName, 
            string password,
            string queueName)
        {
            HostAddress = hostAddress;
            UserName = userName;
            Password = password;
            QueueName = queueName;
        }

        public string HostAddress { get; }
        public string UserName { get; }
        public string Password { get; }
        public string QueueName { get; }
    }
}