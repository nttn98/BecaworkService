using System;
using System.Collections.Generic;

namespace BecaworkService.Models
{
    public class NotificationResponse
    {
        public long Total { get; set; }
        public ICollection<Notification> Data { get; set; }
    }
}
