using System.ComponentModel.DataAnnotations;

namespace StudentApp.Domain.Models.Dtos
{
    public class EnrolmentDataOut
    {
        [Key]
        public int Id { get; set; }
        public int StudentId { get; set; }
        public string UserName { get; set; }
        public int SubjectId { get; set; }
        public string SubjectName { get; set; }
        public double Assignment1 { get; set; }
        public double Assignment2 { get; set; }
        public double Assignment3 { get; set; }
        public double Test { get; set; }
        public double FinalExam { get; set; }
        public double Average { get; set; }
    }
}
