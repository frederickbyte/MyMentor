using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyMentor.DataAccessLayer.Models
{
    [Table("StudentMentor")]
    public class StudentMentor : AuditableEntity
    {
        public StudentMentor()
        {
            
        }

        [Key]
        public Guid Id { get; set; }
        public string StudentId { get; set; }
        public ApplicationUser Student { get; set; }
        public string MentorId { get; set; }
        public ApplicationUser Mentor { get; set; }
        // String for now (Approved, Denied or null)
        public string ApprovalStatus { get; set; } 
    }
}