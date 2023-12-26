using EcomApp.Domain.Infrastructure;
using EcomApp.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcomApp.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly EcomAppDbContext _context;
        private bool disposed = false;

        public IProductRepository Products { get; private set; }
        public IOrderRepository Orders { get; private set; }

        public UnitOfWork(EcomAppDbContext context, IProductRepository productRepository, IOrderRepository orderRepository)
        {
            _context = context;
            Products = productRepository;
            Orders = orderRepository;
        }

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}