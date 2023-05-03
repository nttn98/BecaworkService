using System;

namespace BecaworkService.Models
{
    public class ElectrolyticToken
    {
        public long Id { get; set; }
        public string Request { get; set; }
        public string Response { get; set; }
        public int? StatusCode { get; set; }
        public DateTime? CreatedTime { get; set; }
        public DateTime? LastModified { get; set; }
    }
}
