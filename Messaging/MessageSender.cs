using System;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;
using System.Text;

[assembly: FunctionsStartup(typeof(Funda.RealCloudNotifications.Startup))]

namespace Funda.RealCloudNotifications.Messaging
{
    public class MessageSender : IMessageSender
    {
        public async Task SendAsync(IMessage message, string queueName)
        {
            // Encode to json
            var messageType = message.GetType();
            var json = JsonSerializer.Serialize(message, messageType);

            var connectionString = "Endpoint=sb://sb-realcloud-notifications.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=ezp+qCJlFA9c6lnxiX3sVyvOwadx+PpHdytpVGCQhyc=";
            var queueClient = new QueueClient(connectionString, queueName);

            await queueClient.SendAsync(new Message(Encoding.UTF8.GetBytes(json)));
        }
    }
}