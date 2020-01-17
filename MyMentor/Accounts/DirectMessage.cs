using System;
using System.Collections.Generic;
using System.Text;

namespace MyMentor.Accounts
{
    public class DirectMessage
    {
        public Guid Id { get; set; }
        public string SenderId { get; set; }
        public string RecipientId { get; set; }
        public string Message { get; set; }
        public DateTime LastUpdateTime { get; set; }

        public DirectMessage() { }

        public DirectMessage(Guid id, string senderId, string recipientId, string message, DateTime messageDate)
        {
            Id = id;
            SenderId = senderId;
            RecipientId = recipientId;
            Message = message;
            LastUpdateTime = messageDate;
        }
    }
}
