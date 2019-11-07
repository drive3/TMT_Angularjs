using Store.Models;
using Store_My.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Store_My 
{
    public class Data_Store : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Catelogy> catelogies { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
    }
}