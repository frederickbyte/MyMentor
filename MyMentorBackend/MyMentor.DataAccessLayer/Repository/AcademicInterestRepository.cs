using MyMentor.DataAccessLayer.Models;
using MyMentor.DataAccessLayer.Repository.Interfaces;

namespace MyMentor.DataAccessLayer.Repository
{
    public class AcademicInterestRepository: Repository<AcademicInterest>, IAcademicInterestRepository
    {
        public AcademicInterestRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}