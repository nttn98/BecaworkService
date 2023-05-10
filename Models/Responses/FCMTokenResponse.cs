using System.Collections.Generic;

namespace BecaworkService.Models.Responses
{
    public class FCMTokenResponse
    {
        public long Total { get; set; }
        public ICollection<FCMToken> Data { get; set; }
    }
}

