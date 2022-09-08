using NUnit.Framework;
using StudentApp.Controllers;
using StudentApp.Domain.Data;
using StudentApp.Domain.Models;
using StudentApp.Infrastructure.Persistence.Context;
using FluentAssertions;

namespace StudentApp.UnitTests.UnitTests
{
    public class Tests
    {
        [Test]
        public async Task Test_GetEnrolmentById()
        {
            var _context = new BackendDbContext();
            var _backendRepo = new BackendRepo(_context);

            Enrolment enrolment = new Enrolment
            {
                Id = 46,
                StudentId = 96,
                UserName = "Dominic Nguyen",
                SubjectId = 48,
                SubjectName = "Compsci 210",
                Assignment1 = 60,
                Assignment2 = 60,
                Assignment3 = 60,
                Test = 50,
                FinalExam = 70,
                Average = 60
            };

            Enrolment enrolment1 = _backendRepo.GetEnrolmentById(46);
            enrolment1.UserName.Should().Be(enrolment.UserName);
            enrolment1.SubjectId.Should().NotBe(38);
        }

    }
}