using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyMentor.DataAccessLayer.Models
{
    [Table("AcademicInterest")]
    public class AcademicInterest : AuditableEntity
    {
        public AcademicInterest()
        {

        }
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}