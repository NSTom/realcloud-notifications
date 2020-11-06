using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Funda.RealCloudNotifications.Messaging;

[assembly: FunctionsStartup(typeof(Funda.RealCloudNotifications.Startup))]

namespace Funda.RealCloudNotifications.Entries
{
    public class ManualNotificationEntry
    {
        private readonly IMessageSender _messageBus;

        public ManualNotificationEntry(IMessageSender messageBus)
        {
            _messageBus = messageBus;
        }

        [FunctionName("ManualNotificationEntry")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string name = req.Query["name"];

            // Post a message to the service bus
            var message = new NotificationMessage
            {
                Name = name,
                Greeting = "Good afternoon"
            };
            await _messageBus.SendAsync(message, "NotificationsQueue");

            // Do the default behaviour
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            name = name ?? data?.name;

            string responseMessage = string.IsNullOrEmpty(name)
                ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
                : $"Hello, {name}. This HTTP triggered function executed successfully.";

            return new OkObjectResult(responseMessage);
        }
    }
}
