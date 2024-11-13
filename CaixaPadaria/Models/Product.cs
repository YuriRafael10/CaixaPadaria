using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CaixaPadaria.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Required]
        [StringLength(100)]
        public string? Name { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        [Range(0, double.MaxValue, ErrorMessage = "O preço de custo não pode ser negativo.")]
        public decimal CostPrice { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        [Range(0, double.MaxValue, ErrorMessage = "O preço de venda não pode ser negativo.")]
        public decimal SalePrice { get; set; }

        [Range(0, int.MaxValue)]
        public int Quantity { get; set; } = 0;

        [Required]
        [StringLength(13, MinimumLength = 8, ErrorMessage = "O código de barras deve ter entre 8 e 13 dígitos.")]
        public string? Barcode { get; set; }

        // Relacionamento com Marca
        public int BrandId { get; set; }
        public Brand? Brand { get; set; }

        // Relacionamento com Categoria
        public int CategoryId { get; set; }
        public Category? Category { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        [Range(0, double.MaxValue)]
        public decimal TotalSold { get; set; } = 0;
    }
}