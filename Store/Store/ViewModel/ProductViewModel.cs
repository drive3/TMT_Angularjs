using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Store.ViewModel
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string CodeProduct { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Note { get; set; }
        [Required]
        public DateTime DateCreated { get; set; }
        public int CatelogyID { get; set; }
        public string CatelogyName { get; set; }
    }
}