using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Funda.RealCloudNotifications.Messaging;

[assembly: FunctionsStartup(typeof(Funda.RealCloudNotifications.Startup))]

namespace Funda.RealCloudNotifications
{
  public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddTransient<IMessageSender, MessageSender>();
        }
    }
}