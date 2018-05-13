using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Alkobazar.Models.DTOs
{
    public class OrderDTO
    {
        public Order Order { get; set; }
        public List<Order_Items> Order_Items { get; set; }
        
        public OrderDTO(Order order, List<Order_Items> order_Items)
        {
            this.Order = order;
            this.Order_Items = order_Items;
        }

        public OrderDTO(){ }

    }
}