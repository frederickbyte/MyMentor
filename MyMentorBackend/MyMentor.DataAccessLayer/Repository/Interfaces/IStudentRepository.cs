using System.Collections;
using MyMentor.DataAccessLayer.Models;

namespace MyMentor.DataAccessLayer.Repository.Interfaces
{
    public interface IStudentRepository : IRepository<Student>
    {
        //IEnumerable<Mentorship> GetStudentsIAmMentoring(string studentId);
        //IEnumerable<Mentorship> GetMyMentors(string studentId);
        Student GetStudentForUser(string userId);
    }

    
}