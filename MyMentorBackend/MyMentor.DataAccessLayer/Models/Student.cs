using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyMentor.DataAccessLayer.Models
{
    [Table("Student")]
    public class Student : AuditableEntity
    {
        public Student()
        {

        }
        [Key]
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public string University { get; set; }
        public string Grade { get; set; }
        public Guid MajorId { get; set; }
        public AcademicInterest Major { get; set; }
    }
}