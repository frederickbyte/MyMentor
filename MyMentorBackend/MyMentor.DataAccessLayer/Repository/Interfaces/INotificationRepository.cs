using System.Collections;
using System.Collections.Generic;
using MyMentor.DataAccessLayer.Models;

namespace MyMentor.DataAccessLayer.Repository.Interfaces
{
    public interface INotificationRepository : IRepository<Notification>
    {
        IEnumerable<Notification> GetNotificationsForUser(string userId);

        void AddNotification(string senderId, string recipientId, string body);

    }
}