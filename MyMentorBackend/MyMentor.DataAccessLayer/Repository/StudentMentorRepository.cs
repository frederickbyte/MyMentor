using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MyMentor.DataAccessLayer.Models;
using MyMentor.DataAccessLayer.Repository.Interfaces;

namespace MyMentor.DataAccessLayer.Repository
{
    public class StudentMentorRepository : Repository<StudentMentor>, IStudentMentorRepository
    {
        public StudentMentorRepository(ApplicationDbContext context) : base(context)
        {
        }

        public IEnumerable<StudentMentor> GetMentorsForStudent(string studentUserId)
        {
            return _context.StudentMentors
                .Include(sm => sm.Mentor)
                .ThenInclude(m => m.Teacher)
                .ThenInclude(t => t.Degree)
                .Include(sm => sm.Mentor)
                .ThenInclude(u => u.UserInterests)
                .ThenInclude(ui => ui.AcademicInterest)
                .Where(sm => sm.StudentId == studentUserId && sm.ApprovalStatus == "Approved");

        }

        public IEnumerable<StudentMentor> GetRequestedMentorsForStudent(string studentUserId)
        {
            return _context.StudentMentors
                .Include(sm => sm.Mentor)
                .ThenInclude(m => m.Teacher)
                .ThenInclude(t => t.Degree)
                .Include(sm => sm.Mentor)
                .ThenInclude(u => u.UserInterests)
                .ThenInclude(ui => ui.AcademicInterest)
                .Where(sm => sm.StudentId == studentUserId && sm.ApprovalStatus == "Pending");
        }

        // TODO: Refactor to encapsulate duplicate code in these methods. Andrew Stolzle
        public IEnumerable<StudentMentor> GetMentorRequestsForMentor(string mentorUserId)
        {
            return _context.StudentMentors
                .Include(sm => sm.Student)
                .ThenInclude(m => m.Student)
                .ThenInclude(t => t.Major)
                .Include(sm => sm.Student)
                .ThenInclude(u => u.UserInterests)
                .ThenInclude(ui => ui.AcademicInterest)
                .Where(sm => sm.MentorId == mentorUserId && sm.ApprovalStatus == "Pending");
        }

        public IEnumerable<StudentMentor> GetMentorshipsForMentor(string mentorUserId)
        {
            return _context.StudentMentors
                .Include(sm => sm.Student)
                .ThenInclude(m => m.Student)
                .ThenInclude(t => t.Major)
                .Include(sm => sm.Student)
                .ThenInclude(u => u.UserInterests)
                .ThenInclude(ui => ui.AcademicInterest)
                .Where(sm => sm.MentorId == mentorUserId && sm.ApprovalStatus == "Approved");
        }

        public void AcceptMentorship(string mentorUserId, string studentUserId)
        {
            var mentorShip = _context.StudentMentors
                .FirstOrDefault(sm => sm.StudentId == studentUserId && sm.MentorId == mentorUserId);

            if (mentorShip != null)
            {
                mentorShip.ApprovalStatus = "Approved";
                _context.SaveChanges();
            }
        }

        public StudentMentor RequestMentorship(string mentorUserId, string studentUserId)
        {
            var studentMentorship = new StudentMentor();
            studentMentorship.MentorId = mentorUserId;
            studentMentorship.StudentId = studentUserId;
            studentMentorship.ApprovalStatus = "Pending";
            _context.StudentMentors.Add(studentMentorship);
            _context.SaveChanges();

            // Refetch the details and send to the client
            var studentMentor = _context.StudentMentors
                .Include(sm => sm.Mentor)
                .ThenInclude(m => m.Teacher)
                .ThenInclude(t => t.User)
                .ThenInclude(u => u.UserInterests)
                .ThenInclude(ui => ui.AcademicInterest)
                .FirstOrDefault(sm => sm.MentorId == mentorUserId && sm.StudentId == studentUserId);

            return studentMentor;
        }
    }
}