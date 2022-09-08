using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using StudentApp.Domain.Data;
using StudentApp.Domain.Models.Dtos;
using StudentApp.Domain.Models;

namespace StudentApp.Controllers
{

    [Route("api")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IBackendRepo _repository;

        public StudentsController(IBackendRepo repository)
        {
            _repository = repository;
        }

        //POST: api/Students
        [HttpPost("Register")]
        public ActionResult<string> Register(StudentDataIn student)
        {
            if (_repository.GetStudentByUserName(student.UserName) != null || _repository.GetAdminByUserName(student.UserName) != null)
            {
                return Ok("Username not available.");
            }

            Student student1 = new Student { UserName = student.UserName, Password = student.Password };
            Student registed = _repository.RegisterUser(student1);
            return Ok("User successfully registered.");
        }

        // GET: api/Students
        //[Authorize(AuthenticationSchemes = "MyAuthentication")]
        //[Authorize(Policy = "AdminOnly")]
        [HttpGet("GetStudents")]
        public ActionResult<IEnumerable<StudentDataOut>> GetStudents()
        {
            var students = _repository.GetStudents().ToList();
            List<StudentDataOut> studentDataOuts = new List<StudentDataOut>();
            foreach (Student student in students)
            {
                StudentDataOut studentDataOut = new StudentDataOut
                {
                    Id = student.StudentId,
                    UserName = student.UserName,
                    Password = student.Password,
                };
                studentDataOuts.Add(studentDataOut);
            }
            return Ok(studentDataOuts);
        }

        // GET: api/GetAllSubjectOfStudent/{studentId}
        //[HttpGet("GetAllSubjectOfStudent/{studentId}")]
        //public ActionResult<IEnumerable<SubjectDataOut>> GetAllSubjectOfStudent(int studentId)
        //{
        //    if (_repository.GetEnrolmentsByStudentId(studentId) == null)
        //    {
        //        return NotFound();
        //    }
        //    IEnumerable<Enrolment> enrolments = _repository.GetEnrolmentsByStudentId(studentId);
        //    Student s = _repository.GetStudentByStudentId(studentId);
        //    List<SubjectDataOut> subjectDataOuts = new List<SubjectDataOut>();
        //    for (int i = 0; i < enrolments.Count(); i++)
        //    {
        //        var e = enrolments.ElementAt(i);
        //        var subject = _repository.GetSubjectById(e.SubjectId); 
        //        SubjectDataOut sdo = new SubjectDataOut
        //        {
        //            SubjectId = subject.SubjectId,
        //            UserName = s.UserName,
        //            SubjectName = subject.SubjectName,
        //            Assignment1 = subject.Assignment1,
        //            Assignment2 = subject.Assignment2,
        //            Assignment3 = subject.Assignment3,
        //            Test = subject.Test,
        //            FinalExam = subject.FinalExam,
        //            Average = subject.Average
        //        };
        //        subjectDataOuts.Add(sdo);
        //    }
        //    return Ok(subjectDataOuts);
        //}

        //[Authorize(AuthenticationSchemes = "MyAuthentication")]
        //[Authorize(Policy = "AuthOnly")]
        [HttpPost("EnrollSubject")]
        public ActionResult<string> EnrollSubject(SubjectDataIn subject)
        {
            //ClaimsIdentity ci = HttpContext.User.Identities.FirstOrDefault();
            //Claim c = ci.FindFirst("normalUser");
            //string userName = c.Value;
            Student student = _repository.GetStudentByUserName(subject.UserName);
            Subject s = new Subject
            {
                SubjectName = subject.SubjectName
            };
            Subject addedSubject = _repository.AddNewSubject(s);

            Enrolment enrolment = new Enrolment
            {
                StudentId = student.StudentId,
                Student = student,
                UserName = student.UserName,
                SubjectId = addedSubject.SubjectId,
                Subject = addedSubject,
                SubjectName = addedSubject.SubjectName
            };
            Enrolment addedEnrolment = _repository.AddNewEnrolment(enrolment);
            return Ok("Enroll Success");
        }

        //[Authorize(AuthenticationSchemes = "MyAuthentication")]
        //[Authorize(Policy = "AdminOnly")]
        //[HttpGet("GetEnrolment/{id}")]
        //public ActionResult<EnrolmentDataOut> GetEnrolment(int id)
        //{
        //    if (_repository.GetEnrolmentById(id) == null)
        //    {
        //        return NotFound();
        //    }
        //    Enrolment enrolment = _repository.GetEnrolmentById(id);
        //    EnrolmentDataOut e = new EnrolmentDataOut
        //    {
        //        Id = enrolment.Id,
        //        StudentId = enrolment.StudentId,
        //        SubjectId=enrolment.SubjectId
        //    };
        //    return Ok(e);
        //}

        //[Authorize(AuthenticationSchemes = "MyAuthentication")]
        //[Authorize(Policy = "AdminOnly")]
        [HttpGet("GetEnrolmentsOfStudent/{studentId}")]
        public ActionResult<IEnumerable<EnrolmentDataOut>> GetEnrolmentsOfStudent(int studentId)
        {
            if (_repository.GetEnrolmentsByStudentId(studentId) == null)
            {
                return NotFound();
            }
            IEnumerable<Enrolment> enrolments = _repository.GetEnrolmentsByStudentId(studentId);
            List<EnrolmentDataOut> enrolmentDataOuts = new List<EnrolmentDataOut>();
            foreach (Enrolment e in enrolments)
            {
                EnrolmentDataOut enrolmentDataOut = new EnrolmentDataOut
                {
                    Id = e.Id,
                    StudentId = e.StudentId,
                    UserName = e.UserName,
                    SubjectId = e.SubjectId,
                    SubjectName=e.SubjectName,
                    Assignment1=e.Assignment1,
                    Assignment2=e.Assignment2,
                    Assignment3=e.Assignment3, 
                    Test=e.Test,
                    FinalExam=e.FinalExam,
                    Average=e.Average
                };
                enrolmentDataOuts.Add(enrolmentDataOut);
            }
            return Ok(enrolmentDataOuts);
        }

        //[Authorize(AuthenticationSchemes = "MyAuthentication")]
        //[Authorize(Policy = "AdminOnly")]
        //[HttpGet("GetEnrolmentsOfSubject/{subjectId}")]
        //public ActionResult<IEnumerable<EnrolmentDataOut>> GetEnrolmentsOfSubject(int subjectId)
        //{
        //    if (_repository.GetEnrolmentsBySubjectId(subjectId) == null)
        //    {
        //        return NotFound();
        //    }
        //    IEnumerable<Enrolment> enrolments = _repository.GetEnrolmentsBySubjectId(subjectId);
        //    List<EnrolmentDataOut> enrolmentDataOuts = new List<EnrolmentDataOut>();
        //    foreach (Enrolment e in enrolments)
        //    {
        //        EnrolmentDataOut enrolmentDataOut = new EnrolmentDataOut
        //        {
        //            Id = e.Id,
        //            StudentId = e.StudentId,
        //            UserName = e.UserName,
        //            SubjectId = e.SubjectId,
        //            SubjectName = e.SubjectName,
        //            Assignment1 = e.Assignment1,
        //            Assignment2 = e.Assignment2,
        //            Assignment3 = e.Assignment3,
        //            Test = e.Test,
        //            FinalExam = e.FinalExam,
        //            Average = e.Average
        //        };
        //        enrolmentDataOuts.Add(enrolmentDataOut);
        //    }
        //    return Ok(enrolmentDataOuts);
        //}

        //[Authorize(AuthenticationSchemes = "MyAuthentication")]
        //[Authorize(Policy = "AdminOnly")]
        [HttpPut("UpdateSubjectOfEnrolment/{enrolmentId}")]
        public ActionResult<EnrolmentDataOut> UpdateSubjectOfEnrolment(int enrolmentId, EnrollmentDataIn enrolment)
        {
            if (_repository.GetEnrolmentById(enrolmentId) == null)
            {
                return NotFound();
            }
            Enrolment e = _repository.GetEnrolmentById(enrolmentId);
            e.Assignment1 = enrolment.Assignment1;
            e.Assignment2 = enrolment.Assignment2;
            e.Assignment3 = enrolment.Assignment3;
            e.Test = enrolment.Test;
            e.FinalExam = enrolment.FinalExam;
            e.Average = Math.Round(((e.Assignment1 + e.Assignment2 + e.Assignment3 + e.Test + e.FinalExam) * 0.2), 2);
            Enrolment updatedEnrolment = _repository.UpdateEnrolment(e);
            EnrolmentDataOut sdo = new EnrolmentDataOut
            {
                Id = updatedEnrolment.Id,
                StudentId = updatedEnrolment.StudentId,
                UserName = updatedEnrolment.UserName,
                SubjectId = updatedEnrolment.SubjectId,
                SubjectName = updatedEnrolment.SubjectName,
                Assignment1 = updatedEnrolment.Assignment1,
                Assignment2 = updatedEnrolment.Assignment2,
                Assignment3 = updatedEnrolment.Assignment3,
                Test = updatedEnrolment.Test,
                FinalExam = updatedEnrolment.FinalExam,
                Average = updatedEnrolment.Average
            };
            return Ok(sdo);
        }

        //[Authorize(AuthenticationSchemes = "MyAuthentication")]
        //[Authorize(Policy = "AdminOnly")]
        [HttpDelete("DeleteStudent/{studentId}")]
        public ActionResult<string> DeleteStudent(int studentId)
        {
            Student s = _repository.GetStudentByStudentId(studentId);
            if (s == null)
            {
                return NotFound();
            }
            Student deletedStudent = _repository.DeleteStudent(s);
            return Ok("Successfully Deleted");
        }

        //[Authorize(AuthenticationSchemes = "MyAuthentication")]
        //[Authorize(Policy = "AdminOnly")]
        [HttpDelete("DeleteSubject/{subjectId}")]
        public ActionResult<string> DeleteSubject(int subjectId)
        {
            Subject s = _repository.GetSubjectById(subjectId);
            if (s == null)
            {
                return NotFound();
            }
            Subject deletedSubject = _repository.DeleteSubject(s);
            return Ok("Successfully Deleted");
        }
    }
}
