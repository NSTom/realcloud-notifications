using Microsoft.Azure.Functions.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(Funda.RealCloudNotifications.Startup))]

namespace Funda.RealCloudNotifications.Messaging
{
    public interface IMessageOpener
    {
        T Open<T>(string unopenedMessage) where T : IMessage;
    }
}