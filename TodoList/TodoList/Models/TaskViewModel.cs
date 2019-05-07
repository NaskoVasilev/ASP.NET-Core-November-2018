using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoList.Models
{
    public class TaskViewModel
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public DateTime StratDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
