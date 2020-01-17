using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyMentor.DataAccessLayer.Models
{
    [Table("UserInterest")]
    public class UserInterest : AuditableEntity
    {
        public UserInterest()
        {
            
        }
        [Key]
        public Guid Id { get; set; }

        public Guid AcademicInterestId { get; set; }
        public AcademicInterest AcademicInterest { get; set; }
        public string UserId { get; set; }
    }
}