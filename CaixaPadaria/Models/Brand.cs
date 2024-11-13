using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaixaPadaria.Models
{
    public class Brand
    {
        [Key]
        public int BrandId { get; set; }

        [Required]
        [StringLength(50)]
        public string? Name { get; set; }

        public decimal TotalSales { get; set; }

        public ICollection<Product> Products { get; set; }

        public Brand()
        {
            Products = new List<Product>();
        }
    }
}
