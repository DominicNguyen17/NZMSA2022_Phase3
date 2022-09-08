using System.ComponentModel.DataAnnotations;

namespace StudentApp.Domain.Models
{
    public class Subject
    {
        [Key]
        public int SubjectId { get; set; }
        public string SubjectName { get; set; }
        public ICollection<Enrolment> Enrolments { get; set; }
    }
}
