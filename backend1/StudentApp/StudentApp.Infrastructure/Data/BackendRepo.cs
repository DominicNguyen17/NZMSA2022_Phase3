using Microsoft.EntityFrameworkCore.ChangeTracking;
using StudentApp.Domain.Models;
using StudentApp.Infrastructure.Persistence.Context;

namespace StudentApp.Domain.Data
{
    public class BackendRepo : IBackendRepo
    {
        private readonly BackendDbContext _dbContext;
        public BackendRepo(BackendDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Student RegisterUser(Student student)
        {
            EntityEntry<Student> e = _dbContext.Students.Add(student);
            Student s = e.Entity;
            _dbContext.SaveChanges();
            return s;
        }

        public bool ValidLogin(string userName, string password)
        {
            Student student = _dbContext.Students.FirstOrDefault(e => e.UserName == userName && e.Password == password);
            if (student == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool ValidAdminLogin(string userName, string password)
        {
            Admin admin = _dbContext.Admins.FirstOrDefault(e => e.UserName == userName && e.Password == password);
            if (admin == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public Student GetStudentByUserName(string userName)
        {
            Student s = _dbContext.Students.FirstOrDefault(e => e.UserName == userName);
            return s;
        }
        public Admin GetAdminByUserName(string userName)
        {
            Admin a = _dbContext.Admins.FirstOrDefault(e => e.UserName == userName);
            return a;
        }

        public IEnumerable<Student> GetStudents()
        {
            return _dbContext.Students.ToList();
        }

        public Subject GetSubjectById(int id)
        {
            Subject s = _dbContext.Subjects.FirstOrDefault(e => e.SubjectId == id);
            return s;
        }

        public Subject AddNewSubject(Subject subject)
        {
            EntityEntry<Subject> e = _dbContext.Subjects.Add(subject);
            Subject s = e.Entity;
            _dbContext.SaveChanges();
            return s;
        }

        public Enrolment UpdateEnrolment(Enrolment enrolment)
        {
            EntityEntry<Enrolment> e = _dbContext.Enrolments.Update(enrolment);
            Enrolment s = e.Entity;
            _dbContext.SaveChanges();
            return s;
        }

        public Enrolment AddNewEnrolment(Enrolment enrolment)
        {
            EntityEntry<Enrolment> e = _dbContext.Enrolments.Add(enrolment);
            Enrolment s = e.Entity;
            _dbContext.SaveChanges();
            return s;
        }

        public Enrolment GetEnrolmentById(int id)
        {
            Enrolment s = _dbContext.Enrolments.FirstOrDefault(e => e.Id == id);
            return s;
        }

        public IEnumerable<Enrolment> GetEnrolmentsByStudentId(int id)
        {
            IEnumerable<Enrolment> s = _dbContext.Enrolments.Where(e => e.StudentId == id);
            return s;
        }

        public IEnumerable<Enrolment> GetEnrolmentsBySubjectId(int id)
        {
            IEnumerable<Enrolment> s = _dbContext.Enrolments.Where(e => e.SubjectId == id);
            return s;
        }

        public Student GetStudentByStudentId(int id)
        {
            Student s = _dbContext.Students.FirstOrDefault(e => e.StudentId == id);
            return s;
        }

        public Student DeleteStudent(Student student)
        {
            EntityEntry<Student> e = _dbContext.Students.Remove(student);
            Student s = e.Entity;
            _dbContext.SaveChanges();
            return s;
        }

        public Subject DeleteSubject(Subject subject)
        {
            EntityEntry<Subject> e = _dbContext.Subjects.Remove(subject);
            Subject s = e.Entity;
            _dbContext.SaveChanges();
            return s;
        }
    }
}
