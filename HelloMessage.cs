using System;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Funda.RealCloudNotifications.Messaging;

[assembly: FunctionsStartup(typeof(Funda.RealCloudNotifications.Startup))]

namespace Funda.RealCloudNotifications
{
  public class NotificationMessage : IMessage
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Greeting { get; set; }

        public bool Equals(IMessage obj)
        {
            return obj is NotificationMessage message &&
                  Id.Equals(message.Id) &&
                  Name == message.Name &&
                  Greeting == message.Greeting;
        }
  }
}