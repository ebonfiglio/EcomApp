using EcomApp.Application.DTOs;
using EcomApp.Application.Services;
using EcomApp.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace EcomApp.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IMessagingService _messagingService;

        public OrderController(IOrderService orderService, IMessagingService messagingService)
        {
            _orderService = orderService;
            _messagingService = messagingService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] OrderDTO orderDto)
        {
            var orderId = await _orderService.CreateOrderAsync(orderDto);
            if (orderId == Guid.Empty)
            {
                return BadRequest("Failed to create order");
            }

            // Proceed to publish the message for processing
            await _messagingService.SendMessageAsync(orderDto, "orders-queue");

            // Return a confirmation response
            return Ok(new { OrderId = orderId });
        }
    }

}
