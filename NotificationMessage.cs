using System;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Funda.RealCloudNotifications.Messaging;

[assembly: FunctionsStartup(typeof(Funda.RealCloudNotifications.Startup))]

namespace Funda.RealCloudNotifications
{
  public class NotificationMessage : IMessage
    {
        public string Name { get; set; }
        public string Greeting { get; set; }

        public bool Equals(IMessage obj)
        {
            return obj is NotificationMessage message &&
                  Name == message.Name &&
                  Greeting == message.Greeting;
        }
  }
}