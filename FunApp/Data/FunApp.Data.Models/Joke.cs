using FunApp.Data.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace FunApp.Data.Models
{
    public class Joke : BaseModel<int>
    {
        public string Content { get; set; }

        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }

        public int TotalRating { get; set; }

        public int RatingVotes { get; set; }

        [NotMapped]
        public double Rating
        {
            get
            {
                if(this.RatingVotes == 0)
                {
                    return 0;
                }
                return (double)TotalRating / RatingVotes;
            }
        }
    }
}
