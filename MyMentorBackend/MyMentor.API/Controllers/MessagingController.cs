using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyMentor.API.DTOs;
using MyMentor.DataAccessLayer.Models;
using MyMentor.DataAccessLayer.Repository.Interfaces;

namespace MyMentor.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagingController : ControllerBase
    {
        private IUnitOfWork _unitOfWork;
        public MessagingController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        //[HttpPost("/sendMessage")]
        //public void SendMessage([FromBody] NotificationDTO message)
        //{
        //    _unitOfWork.Notifications.AddNotification(message.SenderId, message.RecipientId, message.Message);
        //}

        [HttpGet("sendMessage/{senderId}/{recipientId}/{message}")]
        public void SendMessage(string senderId, string recipientId, string message)
        {
            _unitOfWork.Notifications.AddNotification(senderId, recipientId, message);
            _unitOfWork.SaveChanges();
        }

        /// <summary>
        /// Retrieves all messages from every person for the user. DEMO only
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet("{userId}/getMessages")]
        public ActionResult<IEnumerable<NotificationDTO>> GetMessagesForUser(string userId)
        {
            var notifications = _unitOfWork.Notifications.GetNotificationsForUser(userId)
                .Select(n => new NotificationDTO(n)).ToList();
            return notifications;
        }
    }

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
            LastUpdateTime = notification.UpdatedDate;
        }

        public Guid Id { get; set; }
        public string SenderId { get; set; }
        public string RecipientId { get; set; }
        public string Message { get; set; }
        public DateTime LastUpdateTime { get; set; }
    }
}