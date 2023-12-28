using EcomApp.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcomApp.Application.Services
{
    public interface IProductService
    {
        Task<Guid> AddProductAsync(ProductDTO productDto);
        Task<ProductDTO> GetProductByIdAsync(Guid id);
        Task UpdateProductAsync(Guid id, ProductDTO productDto);
        Task DeleteProductAsync(Guid id);
        Task<IEnumerable<ProductDTO>> GetAllProductsAsync();
    }

}
