using EcomApp.Application.DTOs;
using EcomApp.Application.Services;
using EcomApp.Domain.Entities;
using EcomApp.Domain.Enums;
using EcomApp.Domain.Infrastructure;
using EcomApp.Domain.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcomApp.UnitTest
{
    public class OrderServiceUnitTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IOrderRepository> _mockOrderRepository;
        private readonly OrderService _orderService;

        public OrderServiceUnitTests()
        {
            // Mock the dependencies
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockOrderRepository = new Mock<IOrderRepository>();

            // Set up the service with mocked dependencies
            _orderService = new OrderService(_mockUnitOfWork.Object);
            _mockUnitOfWork.SetupGet(u => u.Orders).Returns(_mockOrderRepository.Object);
        }

        [Fact]
        public async Task CreateOrderAsync_CreatesAndSavesOrder()
        {
            // Arrange
            var newOrderDto = new OrderDTO
            {
                CustomerId = Guid.NewGuid(),
                OrderDate = DateTime.Now,
                Status = OrderStatus.Pending.ToString(),
                Items = new List<OrderItemDTO>
                {
                    new OrderItemDTO
                    {
                        ProductId = Guid.NewGuid(),
                        Quantity = 1,
                        UnitPrice = 5
                    }
                }
            };

            // Act
            var result = await _orderService.CreateOrderAsync(newOrderDto);

            // Assert
            Assert.NotEqual(Guid.Empty, result);
            _mockOrderRepository.Verify(repo => repo.Add(It.IsAny<Order>()), Times.Once);
            _mockUnitOfWork.Verify(u => u.CommitAsync(), Times.Once);
        }
    }
}
