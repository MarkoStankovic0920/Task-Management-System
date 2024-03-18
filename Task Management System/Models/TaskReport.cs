using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace User_Roles_Application.Models
{
    public class TaskReport
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        [ForeignKey("TaskId")]
        public int TaskId { get; set; }

        public Tasks Task { get; set; }

        [Required]
        public string Description { get; set; }
    }
}