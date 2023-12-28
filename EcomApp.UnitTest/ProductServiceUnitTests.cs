using EcomApp.Application.Services;
using EcomApp.Domain.Entities;
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
    public class ProductServiceUnitTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IProductRepository> _mockProductRepository;
        private readonly ProductService _productService;

        public ProductServiceUnitTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockProductRepository = new Mock<IProductRepository>();

            _productService = new ProductService(_mockUnitOfWork.Object);
            _mockUnitOfWork.SetupGet(u => u.Products).Returns(_mockProductRepository.Object);
        }

        [Fact]
        public async Task GetAllProductsAsync_ReturnsAllProducts()
        {
            // Arrange
            var fakeProducts = new List<Product>
        {
            new Product("Product 1", "Description 1", 100),
            new Product("Product 2", "Description 2", 200)
        };
            _mockProductRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(fakeProducts);

            // Act
            var result = await _productService.GetAllProductsAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(fakeProducts.Count, result.Count());
        }
    }
}
