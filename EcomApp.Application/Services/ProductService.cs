using EcomApp.Application.DTOs;
using EcomApp.Domain.Entities;
using EcomApp.Domain.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcomApp.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // Create
        public async Task<Guid> AddProductAsync(ProductDTO productDto)
        {
            var product = new Product(productDto.Name, productDto.Description, productDto.Price);
            _unitOfWork.Products.Add(product);
            await _unitOfWork.CommitAsync();
            return product.Id;
        }

        // Read
        public async Task<ProductDTO> GetProductByIdAsync(Guid id)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(id);
            return product == null ? null : new ProductDTO
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price
            };
        }

        // Update
        public async Task UpdateProductAsync(Guid id, ProductDTO productDto)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(id);
            if (product != null)
            {
                product.UpdateDetails(productDto.Name, productDto.Description, productDto.Price);
                _unitOfWork.Products.Update(product);
                await _unitOfWork.CommitAsync();
            }
        }

        // Delete
        public async Task DeleteProductAsync(Guid id)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(id);
            if (product != null)
            {
                _unitOfWork.Products.Delete(product);
                await _unitOfWork.CommitAsync();
            }
        }

        // List
        public async Task<IEnumerable<ProductDTO>> GetAllProductsAsync()
        {
            var products = await _unitOfWork.Products.GetAllAsync();
            return products.Select(p => new ProductDTO
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price
            });
        }
    }

}
