using System;

namespace BecaworkService.Models
{
    public class Notification
    {
        public long Id { get; set; }
        public DateTime CreatedTime { get; set; }
        public string Type { get; set; }
        public string Content { get; set; }
        public bool IsRead { get; set; }
        public string Email { get; set; }
        public DateTime? LastModified { get; set; }
        public string From { get; set; }
        public string Url { get; set; }
        public bool? IsSeen { get; set; }

    }
}
