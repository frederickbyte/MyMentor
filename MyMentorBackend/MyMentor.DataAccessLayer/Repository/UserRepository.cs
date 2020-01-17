using System.Linq;
using Microsoft.EntityFrameworkCore;
using MyMentor.DataAccessLayer.Models;
using MyMentor.DataAccessLayer.Repository.Interfaces;

namespace MyMentor.DataAccessLayer.Repository
{
    public class UserRepository : Repository<ApplicationUser>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context)
        {

        }

        public ApplicationUser GetUserWithDetails(string userName, string password)
        {
            return _context.Users
                .Include(u => u.UserInterests)
                .ThenInclude(ui => ui.AcademicInterest)
                .Include(u => u.Teacher)
                .ThenInclude(t => t.Degree)
                .Include(u => u.Student)
                .ThenInclude(s => s.Major)
                .FirstOrDefault(u => u.UserName == userName && u.PasswordHash == password);
        }
    }
}
