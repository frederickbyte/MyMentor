using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MyMentor.DataAccessLayer.Models;
using MyMentor.DataAccessLayer.Repository.Interfaces;

namespace MyMentor.DataAccessLayer.Repository
{
    public class NotificationRepository : Repository<Notification>, INotificationRepository
    {
        public NotificationRepository(ApplicationDbContext context) : base(context)
        {
        }

        public IEnumerable<Notification> GetNotificationsForUser(string userId)
        {
            return _context.Notifications
                .Include(n => n.Recipient)
                .Include(n => n.Sender)
                .Where(n => n.RecipientId == userId || n.SenderId == userId);
        }

        public void AddNotification(string senderId, string recipientId, string message)
        {
            _context.Notifications.Add(new Notification {Id = Guid.NewGuid(), RecipientId = recipientId, SenderId = senderId, Message = message});
            _context.SaveChanges();
        }
    }
}