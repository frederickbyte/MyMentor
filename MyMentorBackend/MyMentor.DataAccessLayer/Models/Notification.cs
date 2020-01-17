using System;
using System.ComponentModel.DataAnnotations;

namespace MyMentor.DataAccessLayer.Models
{
    public class Notification : AuditableEntity
    {
        public Notification()
        {
            
        }
        [Key]
        public Guid Id { get; set; }
        public string SenderId { get; set; }
        public ApplicationUser Sender { get; set; }
        public string RecipientId { get; set; }
        public ApplicationUser Recipient { get; set; }
        public string Message { get; set; }
    }
}