using System;

namespace MyMentor.Accounts
{
    public class Mentorship
    {
        public Guid MentorshipId { get; set; }
        public Guid MentorId { get; set; }
        public Guid MenteeId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string EndedBy { get; set; }
        public string ReasonForEnd { get; set; }
        public Mentorship()
        {

        }
    }
}
