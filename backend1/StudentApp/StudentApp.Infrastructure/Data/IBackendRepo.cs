using StudentApp.Domain.Models;

namespace StudentApp.Domain.Data
{
    public interface IBackendRepo
    {
        Student RegisterUser(Student user);
        Student DeleteStudent(Student student);
        IEnumerable<Student> GetStudents();
        Student GetStudentByUserName(string userName);
        Student GetStudentByStudentId(int id);
        Admin GetAdminByUserName(string userName);
        public bool ValidLogin(string userName, string password);
        public bool ValidAdminLogin(string userName, string password);
        Subject GetSubjectById(int id);
        Subject AddNewSubject(Subject subject);
        Enrolment UpdateEnrolment(Enrolment enrolment);
        Subject DeleteSubject(Subject subject);
        Enrolment AddNewEnrolment(Enrolment enrolment);
        Enrolment GetEnrolmentById(int id);
        IEnumerable<Enrolment> GetEnrolmentsByStudentId(int id);
        IEnumerable<Enrolment> GetEnrolmentsBySubjectId(int id);
    }
}
