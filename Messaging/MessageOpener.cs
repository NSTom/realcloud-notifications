using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using System.Text.Json;

[assembly: FunctionsStartup(typeof(Funda.RealCloudNotifications.Startup))]

namespace Funda.RealCloudNotifications.Messaging
{
    public class MessageOpener : IMessageOpener
    {
        public T Open<T>(string unopenedMessage) where T : IMessage
        {
            return JsonSerializer.Deserialize<T>(unopenedMessage);
        }
    }
}