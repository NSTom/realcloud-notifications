using System;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;
using System.Text;
using Microsoft.Extensions.Configuration;

[assembly: FunctionsStartup(typeof(Funda.RealCloudNotifications.Startup))]

namespace Funda.RealCloudNotifications.Messaging
{
    public class MessageSender : IMessageSender
    {
        private readonly string _serviceBusConnectionString;

        public MessageSender(IConfiguration configuration)
        {
            _serviceBusConnectionString = configuration["ServiceBusConnectionString"];
        }

        public async Task SendAsync(IMessage message, string queueName)
        {
            // Encode to json
            var messageType = message.GetType();
            var json = JsonSerializer.Serialize(message, messageType);

            var queueClient = new QueueClient(_serviceBusConnectionString, queueName);

            await queueClient.SendAsync(new Message(Encoding.UTF8.GetBytes(json)));
        }
    }
}