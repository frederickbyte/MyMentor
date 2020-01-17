using System;
using System.Collections.Generic;
using System.Text;

namespace MyMentor.Accounts
{

    public class CommunityEvent
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public DateTime EventDate { get; set; }

        public CommunityEvent() { }
        public CommunityEvent(string title, string desc, string location, DateTime eventDate)
        {
            Title = title;
            Description = desc;
            Location = location;
            EventDate = eventDate;
        }
    }
}
