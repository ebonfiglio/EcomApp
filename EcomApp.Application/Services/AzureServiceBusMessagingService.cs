using Azure.Messaging.ServiceBus;
using EcomApp.Domain.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcomApp.Application.Services
{
    public class AzureServiceBusMessagingService : IMessagingService
    {
        private readonly string connectionString;

        public AzureServiceBusMessagingService(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public async Task SendMessageAsync<T>(T messageBody, string queueOrTopicName)
        {
            await using var client = new ServiceBusClient(connectionString);
            ServiceBusSender sender = client.CreateSender(queueOrTopicName);

            var message = new ServiceBusMessage(JsonConvert.SerializeObject(messageBody))
            {
                ContentType = "application/json"
            };

            await sender.SendMessageAsync(message);
        }

        // Implement other methods as needed
    }

}
