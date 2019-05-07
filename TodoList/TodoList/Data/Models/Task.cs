using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace TodoList.Data.Models
{
    public class Task
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Content { get; set; }

        public DateTime EndDate { get; set; }

        public DateTime StartDate { get; set; }

        public IdentityUser User { get; set; }
        public string IdentityUserId { get; set; }
    }
}
