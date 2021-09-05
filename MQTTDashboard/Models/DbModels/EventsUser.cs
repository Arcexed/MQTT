using System;
using System.Collections.Generic;

#nullable disable

namespace MQTTDashboard.Models.DbModels
{
    public partial class EventsUser
    {
        public Guid Id { get; set; }
        public Guid IdUser { get; set; }
        public DateTime Date { get; set; }
        public string Message { get; set; }

        public virtual User IdUserNavigation { get; set; }
    }
}
