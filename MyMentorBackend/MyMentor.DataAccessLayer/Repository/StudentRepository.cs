using System.Linq;
using Microsoft.EntityFrameworkCore;
using MyMentor.DataAccessLayer.Models;
using MyMentor.DataAccessLayer.Repository.Interfaces;

namespace MyMentor.DataAccessLayer.Repository
{
    public class StudentRepository : Repository<Student>, IStudentRepository
    {
        public StudentRepository(ApplicationDbContext context) : base(context)
        {
        }

        public Student GetStudentForUser(string userId)
        {
            return _context.Students
                .Include(s => s.Major)
                .FirstOrDefault(s => s.UserId == userId);
        }
    }
}