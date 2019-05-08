using System;

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
