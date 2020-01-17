using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MyMentor.DataAccessLayer.Models
{
    [Table("Teacher")]
    public class Teacher : AuditableEntity
    {
        
        public Teacher()
        {
            
        }
        [Key]
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public Guid DegreeId { get; set; }
        public AcademicInterest Degree { get; set; }
        public string DegreeLevel { get; set; }
        public string University { get; set; }
    }
}
