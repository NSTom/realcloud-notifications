using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Funda.RealCloudNotifications.Messaging;

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
            [QueueTrigger("NotificationsQueue", Connection = "Endpoint=sb://sb-realcloud-notifications.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=ezp+qCJlFA9c6lnxiX3sVyvOwadx+PpHdytpVGCQhyc=")] string unopenedMessage, 
            ILogger log)
        {
            // Open the message
            var message = _messageOpener.Open<NotificationMessage>(unopenedMessage);
            log.LogInformation($"Received a notification message with greeting {message.Greeting} to {message.Name}");
        }
    }
}
