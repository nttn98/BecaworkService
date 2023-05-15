using System;

namespace BecaworkService.Models
{
    public class ElectrolyticTokenLog
    {
        public long Id { get; set; }
        public Mail Mail { get; set; }
        public string Request { get; set; }
        public string Response { get; set; }
        public DateTime? CreatedTime { get; set; }
        public DateTime? LastModified { get; set; }

    }
}
