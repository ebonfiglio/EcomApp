using System;
using System.Threading.Tasks;
using EcomApp.Application.DTOs;
using EcomApp.Application.Services;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace EcomApp.Functions
{
    public class OrderProcessingFunction
    {
        private readonly IOrderService _orderService;

        public OrderProcessingFunction(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [FunctionName("ProcessOrder")]
        public async Task Run(
    [ServiceBusTrigger("orders-queue", Connection = "ServiceBusConnection")] string myQueueItem,
    ILogger log)
        {
            var orderDetails = JsonConvert.DeserializeObject<OrderDTO>(myQueueItem);
            if (orderDetails != null)
            {
                var result = await _orderService.ProcessOrderAsync(orderDetails);
                if (result)
                {
                    log.LogInformation($"Order {orderDetails.Id} processed successfully.");
                }
                else
                {
                    log.LogError($"Failed to process order {orderDetails.Id}.");
                }
            }
            else
            {
                log.LogError("Invalid order details received.");
            }
        }
    }
}
