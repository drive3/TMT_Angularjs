﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Store.ViewModel
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public DateTime orderDate { get; set; }
        public int OrderNumber { get; set; }
        public decimal TotalAmount { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerPhoneNumber { get; set; }
        public virtual IList<OrderDetailViewModel> Items { get; set; }
    }
}