using System.Collections;
using MyMentor.DataAccessLayer.Models;

namespace MyMentor.DataAccessLayer.Repository.Interfaces
{
    public interface IUserRepository : IRepository<ApplicationUser>
    {
        ApplicationUser GetUserWithDetails(string userName, string password);
    }
}
