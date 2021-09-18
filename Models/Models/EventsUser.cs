using System;
using System.Collections.Generic;

#nullable disable

namespace Models.Models
{
    public partial class EventsUser
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public DateTime Date { get; set; }
        public string Message { get; set; }
        public bool isSeen { get; set; } = false;

        public User User { get; set; }
    }
}
