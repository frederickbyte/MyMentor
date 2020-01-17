using MyMentor.DataAccessLayer.Models;
using MyMentor.DataAccessLayer.Repository.Interfaces;

namespace MyMentor.DataAccessLayer.Repository
{
    public class UserInterestRepository : Repository<UserInterest>, IUserInterestRepository
    {
        public UserInterestRepository(ApplicationDbContext context) : base(context)
        {
            
        }
    }
}