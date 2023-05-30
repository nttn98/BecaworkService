using System.Collections.Generic;

namespace BecaworkService.Models.Responses
{
    public class ElectrolyticTokenLogResponse
    {
        public long Total { get; set; }
        public ICollection<ElectrolyticTokenLog> Data { get; set; }
    }
}

