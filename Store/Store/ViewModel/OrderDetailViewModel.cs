using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Store.ViewModel
{
    public class OrderDetailViewModel
    {
        public int Id { get; set; }
        public int Quantiy { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int ProductId { get; set; }
        public int OrderId { get; set; }
    }
}