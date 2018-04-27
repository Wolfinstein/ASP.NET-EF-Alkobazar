using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Alkobazar.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "company_name must not be empty !")]
        public string Name { get; set; }

        [Required(ErrorMessage = "shipment_address must not be empty !")]
        public string Shipment_Address { get; set; }

        [Required(ErrorMessage = "customer_phone must not be empty !")]
        public string Phone { get; set; }
                
        public byte[] Logo { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}