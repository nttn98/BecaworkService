﻿using System;

namespace BecaworkService.Models
{
    public class ElectrolyticToken
    {
        public long Id { get; set; }
        public string Token { get; set; }
        public string Email { get; set; }
        public DateTime? CreatedTime { get; set; }
        public DateTime? LastModified { get; set; }
    }
}
