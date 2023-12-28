using EcomApp.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcomApp.Application.Services
{
    public interface IOrderService
    {
        Task<Guid> CreateOrderAsync(OrderDTO orderDto);
        Task<OrderDTO> GetOrderByIdAsync(Guid id);
        Task<bool> ProcessOrderAsync(OrderDTO orderDetails);
    }
}
