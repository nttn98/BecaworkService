using System;
using System.Collections.Generic;

namespace BecaworkService.Models
{
    public class NotificationResponse
    {
        public NotificationResponse(long total, ICollection<Notification> data)
        {
            Total = total;
            Data = data;
        }

        public long Total { get; set; }
        public ICollection<Notification> Data { get; set; }
    }
}
