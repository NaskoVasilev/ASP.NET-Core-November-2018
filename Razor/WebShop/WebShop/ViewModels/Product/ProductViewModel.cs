using System.ComponentModel.DataAnnotations;
using WebShop.Models.Enums;

namespace WebShop.ViewModels.Product
{
    public class ProductViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 3)]
        public string Name { get; set; }

        [Range(typeof(decimal), "0.01", " 79228162514264337593543950335")]
        public decimal Price { get; set; }

        [MaxLength(200)]
        public string Description { get; set; }

        [Required]
        public ProductType Type { get; set; }
    }
}
