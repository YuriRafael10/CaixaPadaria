using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaixaPadaria.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string? Name { get; set; }
        [Required]
        [StringLength(50)]
        public string? Password { get; set; }
        public bool IsAdmin { get; set; }
        public double TotalSales { get; set; } = 0;
    }
}
