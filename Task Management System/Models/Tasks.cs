using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace User_Roles_Application.Models
{
    public class Tasks
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        [Required]
        public DateTime DueDate { get; set; }

        public string Status { get; set; } = "In Progress";

        [AllowNull(ErrorMessage = "The Creator field is required.")]
        public string Creator { get; set; }

        public string AssignedTo { get; set; }
        public string Supervisor { get; set; }
    }
}