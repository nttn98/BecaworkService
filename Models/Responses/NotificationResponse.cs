using System;
using System.Collections.Generic;

namespace BecaworkService.Models
{
    public class NotificationResponse
    {
        private List<Notification> notifications;

    
        public long Total { get; set; }
        public ICollection<Notification> Data { get; set; }
    }
}
