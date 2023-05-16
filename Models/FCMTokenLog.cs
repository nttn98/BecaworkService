﻿using System;

namespace BecaworkService.Models
{
    public class FCMTokenLog
    {
        public long Id { get; set; }
        public string Request { get; set; }
        public string Response { get; set; }
        public Mail Mail { get; set; }
        public DateTime? CreatedTime { get; set; }
        public DateTime? LastModified { get; set; }

    }
}
