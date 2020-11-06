using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Funda.RealCloudNotifications.Messaging;
using System;

[assembly: FunctionsStartup(typeof(Funda.RealCloudNotifications.Startup))]

namespace Funda.RealCloudNotifications.Sinks
{
    public class FirebaseSink
    {
        private readonly IMessageOpener _messageOpener;

        public FirebaseSink(IMessageOpener messageOpener)
        {
            _messageOpener = messageOpener;
        }
        
        [FunctionName("FirebaseSink")]
        public void Run(
            [ServiceBusTrigger("NotificationsQueue", Connection = "ServiceBusConnectionString")]
            string unopenedMessage, 
            Int32 deliveryCount,
            DateTime enqueuedTimeUtc,
            string messageId,
            ILogger log)
        {
            // Open the message
            var message = _messageOpener.Open<NotificationMessage>(unopenedMessage);
            log.LogInformation($"Received a notification message with greeting {message.Greeting} to {message.Name}");
        }
    }
}
