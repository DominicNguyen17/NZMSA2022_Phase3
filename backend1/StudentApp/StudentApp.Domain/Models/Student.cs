using System.ComponentModel.DataAnnotations;

namespace StudentApp.Domain.Models
{
    public class Student
    {
        [Key]
        public int StudentId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public ICollection<Enrolment> Enrolments { get; set; }
    }
}
