using System.ComponentModel.DataAnnotations;
using WebShop.Models.Enums;

namespace WebShop.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 3)]
        public string Name { get; set; }

        [Range(typeof(decimal), "0.01", " 79228162514264337593543950335")]
        public decimal Price { get; set; }

        public string Description { get; set; }

        public ProductType Type { get; set; }
    }
}
