using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Alkobazar.Models
{
    public class Order
    {

        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "customer_id must not be empty !")]
        public System.DateTime Create_timestamp { get; set; }
        [Required(ErrorMessage = "deadline must not be empty !")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public System.DateTime Deadline { get; set; }
        [Required(ErrorMessage = "Order Number must not be empty !")]
        public string Order_Number { get; set; }

        public int CustomerId { get; set; }
        [ForeignKey("CustomerId")]
        public  Customer Customer { get; set; }

        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }



        public virtual IEnumerable<Order_Items> Order_Items { get; set; }


    }
}