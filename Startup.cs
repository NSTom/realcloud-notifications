using System;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Funda.Extensions.Messaging;
using Funda.Extensions.Messaging.Azure;
using Funda.Extensions.Messaging.Configuration;

[assembly: FunctionsStartup(typeof(Funda.RealCloudNotifications.Startup))]

namespace Funda.RealCloudNotifications
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddFundaMessaging()
                .AddFundaMessagingAzureServiceBus()
                .ConfigureEndpoint("RealCloudNotificationsCommands", builder =>
                {
                    builder
                        .ConfigureAzureServiceBusQueue("MyCoolApp", options =>
                        {
                            options.ServiceBusConnectionString = "Endpoint=sb://sb-realcloud-notifications.servicebus.windows.net/;SharedAccessKeyName=FunctionsAccessKey;SharedAccessKey=LIBVgryPHo4Gx5z9latzubKr3z4zC+ddCy5keccLBpw=";
                            options.TopicName = "realcloudnotificationscommands";
                            options.ShouldCreateResources = true;
                        })
                        .ConfigureBroadcast<HelloMessage>();
                });
        }
    }

    public class HelloMessage : IMessage
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Greeting { get; set; }

        public bool Equals(IMessage obj)
        {
            return obj is HelloMessage message &&
                  Id.Equals(message.Id) &&
                  Name == message.Name &&
                  Greeting == message.Greeting;
        }
  }
}