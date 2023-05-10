using System.Collections.Generic;

namespace BecaworkService.Models.Responses
{
    public class MailResponse
    {
        public long Total { get; set; }
        public ICollection<Mail> Data { get; set; }
    }
}

