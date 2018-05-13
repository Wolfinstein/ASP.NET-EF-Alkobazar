using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Alkobazar.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Name must not be empty !")]
        public string Name { get; set; }

        [Required(ErrorMessage = "alcohol_content must not be empty !")]
        public double Alcohol_content { get; set; }

        [Required(ErrorMessage = "sizeInLiters must not be empty !")]
        public double SizeInLiters { get; set; }

        [Required(ErrorMessage = "price must not be empty !")]
        public double Price { get; set; }

        [Required(ErrorMessage = "description must not be empty !")]
        public string Description { get; set; }

        [Required(ErrorMessage = "quantityInStock must not be empty !")]
        public int QuantityInStock { get; set; }

        public virtual IEnumerable<Order_Items> Order_Items { get; set; }
    }
}