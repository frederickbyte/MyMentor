using System;
using MyMentor.DataAccessLayer.Models;

namespace MyMentor.API.DTOs
{
    public class NotificationDTO
    {
        public NotificationDTO()
        {

        }

        public NotificationDTO(Notification notification)
        {
            Id = notification.Id;
            SenderId = notification.SenderId;
            RecipientId = notification.RecipientId;
            Message = notification.Message;
        }

        public Guid Id { get; set; }
        public string SenderId { get; set; }
        public string RecipientId { get; set; }
        public string Message { get; set; }
    }
}