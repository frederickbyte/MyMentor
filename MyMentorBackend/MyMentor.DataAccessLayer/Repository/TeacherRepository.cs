using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using MyMentor.DataAccessLayer.Models;
using MyMentor.DataAccessLayer.Repository.Interfaces;

namespace MyMentor.DataAccessLayer.Repository
{
    public class TeacherRepository : Repository<Teacher>, ITeacherRepository
    {
        // NOTE: This gets constructed in the base class
        public TeacherRepository(ApplicationDbContext context) : base(context)
        { }

        public Teacher GetTeacherForUser(string userId)
        {
            return _context.Teachers
                .Include(t => t.Degree)
                .FirstOrDefault(t => t.UserId == userId);
        }

        public IEnumerable<Teacher> MatchInterest(Guid academicInterestId)
        {
            return _context.Teachers
                .Include(t => t.User)
                .ThenInclude(u => u.UserInterests)
                .ThenInclude(ua => ua.AcademicInterest)
                .Include(t => t.Degree)
                .Where(t => t.DegreeId == academicInterestId
                            || t.User.UserInterests.Any(ui => ui.AcademicInterestId == academicInterestId)
                );
        }
    }
}
