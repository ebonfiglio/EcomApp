using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcomApp.Domain.Enums
{
    public enum OrderStatus
    {
        Pending,    // Order has been created but not processed yet
        Approved,   // Order has been approved for fulfillment
        Shipped,    // Order has been shipped to the customer
        Delivered,  // Order has been delivered to the customer
        Cancelled   // Order has been cancelled
    }
}
