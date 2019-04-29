using FunApp.Data.Common;
using System.Collections.Generic;

namespace FunApp.Data.Models
{
    public class Category : BaseModel<int>
    {
        public Category()
        {
            this.Jokes = new HashSet<Joke>();
        }

        public string Name { get; set; }

        public virtual ICollection<Joke> Jokes { get; set; }
    }
}