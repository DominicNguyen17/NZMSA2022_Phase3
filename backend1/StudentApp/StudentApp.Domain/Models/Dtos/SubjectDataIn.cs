using System.ComponentModel.DataAnnotations;

namespace StudentApp.Domain.Models.Dtos
{
    public class SubjectDataIn
    {
        [Key]
        public int SubjectId { get; set; }
        public string UserName { get; set; }
        public string SubjectName { get; set; }
    }
}
