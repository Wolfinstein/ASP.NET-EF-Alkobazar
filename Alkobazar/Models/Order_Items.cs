using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Alkobazar.Models
{
    public class Order_Items
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "order_quantity must not be empty !")]
        public int Order_Quantity { get; set; }

        public  Order Order { get; set; }
        public  Product Product { get; set; }
    }
}