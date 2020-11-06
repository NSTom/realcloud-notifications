using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using System.Threading.Tasks;

[assembly: FunctionsStartup(typeof(Funda.RealCloudNotifications.Startup))]

namespace Funda.RealCloudNotifications.Messaging
{
    public interface IMessageSender
    {
        Task SendAsync(IMessage message, string queueName);
    }
}