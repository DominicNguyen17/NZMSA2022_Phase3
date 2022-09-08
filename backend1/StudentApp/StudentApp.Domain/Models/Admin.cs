using System.ComponentModel.DataAnnotations;

namespace StudentApp.Domain.Models
{
    public class Admin
    {
        [Key]
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
