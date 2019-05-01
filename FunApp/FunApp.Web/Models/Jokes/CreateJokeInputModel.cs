using System.ComponentModel.DataAnnotations;

namespace FunApp.Web.Models.Jokes
{
    public class CreateJokeInputModel
    {
        [Required]
        [MinLength(10)]
        public string Content { get; set; }

        [ValidCategoryId]
        public int CategoryId { get; set; }
    }
}
