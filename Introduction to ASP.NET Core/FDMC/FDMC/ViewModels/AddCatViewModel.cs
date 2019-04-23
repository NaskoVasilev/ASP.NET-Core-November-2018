using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FDMC.ViewModels
{
    public class AddCatViewModel
    {
        [MinLength(3), MaxLength(30)]
        [Required]
        public string Name { get; set; }

        [Range(0, 20)]
        public int Age { get; set; }


        public string Breed { get; set; }

        [Required]
        [RegularExpression(@"http:\/\/.+|https:\/\/.+")]
        public string ImageUrl { get; set; }
    }
}
