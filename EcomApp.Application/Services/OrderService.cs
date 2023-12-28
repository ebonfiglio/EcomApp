using EcomApp.Application.DTOs;
using EcomApp.Domain.Entities;
using EcomApp.Domain.Enums;
using EcomApp.Domain.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcomApp.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        // Inject any other services if needed, like messaging or logging

        public OrderService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // Create Order
        public async Task<Guid> CreateOrderAsync(OrderDTO orderDto)
        {
            var order = new Order(orderDto.CustomerId);
            foreach (var item in orderDto.Items)
            {
                order.AddItem(item.ProductId, item.Quantity, item.UnitPrice);
            }
            _unitOfWork.Orders.Add(order);
            await _unitOfWork.CommitAsync();

            // Optionally send a message to Azure Service Bus/Queue for further processing
            // MessagingService.SendOrderCreated(order.Id);

            return order.Id;
        }

        // Retrieve Order Details
        public async Task<OrderDTO> GetOrderByIdAsync(Guid id)
        {
            var order = await _unitOfWork.Orders.GetByIdAsync(id);
            if (order == null) return null;

            var orderDto = new OrderDTO
            {
                Id = order.Id,
                CustomerId = order.CustomerId,
                OrderDate = order.OrderDate,
                Status = order.Status.ToString(),
                Items = order.OrderItems.Select(oi => new OrderItemDTO
                {
                    ProductId = oi.ProductId,
                    Quantity = oi.Quantity,
                    UnitPrice = oi.UnitPrice
                }).ToList()
            };
            return orderDto;
        }

        public async Task<bool> ProcessOrderAsync(OrderDTO orderDetails)
        {
            try
            {
                // Retrieve the order entity from the database
                var order = await _unitOfWork.Orders.GetByIdAsync(orderDetails.Id);
                if (order == null)
                {
                    // Log and handle the situation where the order isn't found
                    return false;
                }

                // Update the order status (assuming status is an enum or similar)
                order.UpdateStatus(OrderStatus.Approved); // UpdateStatus is a method in your Order domain entity

                // Persist the changes to the database
                _unitOfWork.Orders.Update(order);
                await _unitOfWork.CommitAsync();

                return true; // Return true if processing is successful
            }
            catch (Exception ex)
            {
                // Log the exception
                // Handle any cleanup or rollback if necessary
                return false; // Return false if there's an error
            }
        }
    }

}
