using System.Collections.Generic;

namespace BecaworkService.Models.Responses
{
    public class FCMTokenLogResponse
    {
        public long Total { get; set; }
        public ICollection<FCMTokenLog> Data { get; set; }
    }
}
