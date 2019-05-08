using System;
using System.ComponentModel.DataAnnotations;

namespace TodoList.Models
{
    public class TaskEditInputModel
    {
        public int Id { get; set; }

        [Required]
        [MinLength(5), MaxLength(250)]
        public string Content { get; set; }

        public DateTime EndDate { get; set; }
    }
}
