using System.Collections.Generic;
using MyMentor.DataAccessLayer.Models;

namespace MyMentor.DataAccessLayer.Repository.Interfaces
{
    public interface IStudentMentorRepository : IRepository<StudentMentor> {
        IEnumerable<StudentMentor> GetMentorsForStudent(string studentUserId);
        IEnumerable<StudentMentor> GetRequestedMentorsForStudent(string studentUserId);
        IEnumerable<StudentMentor> GetMentorRequestsForMentor(string mentorUserId);
        IEnumerable<StudentMentor> GetMentorshipsForMentor(string mentorUserId);
        void AcceptMentorship(string mentorUserId, string studentUserId);
        StudentMentor RequestMentorship(string mentorUserId, string studentUserId);
    }
}