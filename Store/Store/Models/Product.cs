using Store.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Store_My.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string CodeProduct { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; } 
        public string Note { get; set; }
        public DateTime DateCreated { get; set; }

        [ForeignKey("Catelogy")]
        public int CatelogyId { get; set; }
        public virtual Catelogy Catelogy { get; set; }
    }
}