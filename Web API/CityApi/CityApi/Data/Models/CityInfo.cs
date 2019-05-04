using System.ComponentModel.DataAnnotations;

namespace CityApi.Data.Models
{
    public class CityInfo
    {
        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        public string Name { get; set; }

        [Range(250, long.MaxValue)]
        public long Population { get; set; }
    }
}
