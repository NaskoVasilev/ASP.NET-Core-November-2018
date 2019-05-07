using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

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
