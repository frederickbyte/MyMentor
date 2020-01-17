using System;
using System.Collections.Generic;
using System.Text;
using MyMentor.DataAccessLayer.Models;

namespace MyMentor.DataAccessLayer.Repository.Interfaces
{
    public interface ITeacherRepository : IRepository<Teacher>
    {
        Teacher GetTeacherForUser(string userId);
        IEnumerable<Teacher> MatchInterest(Guid academicInterestId);
    }
}
