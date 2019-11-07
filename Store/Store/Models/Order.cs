using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
namespace Store.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime orderDate { get; set; }
        public int OrderNumber { get; set; }
        public decimal TotalAmount { get; set; }

        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual IList<OrderDetail> Items { get; set; }
    }
}
