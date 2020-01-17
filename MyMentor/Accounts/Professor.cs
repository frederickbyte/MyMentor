using System;
using System.Collections.Generic;

namespace MyMentor.Accounts
{
    public class Professor
    {
        public Guid ProfessorId { get; set; }
        public string University { get; set; }
        public List<string> Courses { get; set; }

        public Professor()
        {

        }
    }
}
