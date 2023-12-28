using EcomApp.Domain.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcomApp.UnitTest
{
    public class MessagingServiceUnitTests
    {
        private readonly Mock<IMessagingService> _mockMessagingService;
        private readonly string testQueueName = "test-queue";

        public MessagingServiceUnitTests()
        {
            _mockMessagingService = new Mock<IMessagingService>();
        }

        [Fact]
        public async Task SendMessageAsync_SendsMessageToCorrectQueue()
        {
            // Arrange
            var testMessage = new { Content = "Hello, World!" };

            // Act
            await _mockMessagingService.Object.SendMessageAsync(testMessage, testQueueName);

            // Assert
            _mockMessagingService.Verify(
                ms => ms.SendMessageAsync(It.IsAny<object>(), testQueueName), Times.Once);
        }
    }

}
