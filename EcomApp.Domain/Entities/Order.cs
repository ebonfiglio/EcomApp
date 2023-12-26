using EcomApp.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcomApp.Domain.Entities
{
    public class Order
    {
        public Guid Id { get; private set; }
        public Guid CustomerId { get; private set; }
        public DateTime OrderDate { get; private set; }
        public List<OrderItem> OrderItems { get; private set; }
        public OrderStatus Status { get; private set; }

        public Order(Guid customerId)
        {
            Id = Guid.NewGuid();
            CustomerId = customerId;
            OrderDate = DateTime.UtcNow;
            OrderItems = new List<OrderItem>();
            Status = OrderStatus.Pending;
        }
    }

}
