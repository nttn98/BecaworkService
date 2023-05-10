using System.Collections.Generic;

namespace BecaworkService.Models.Responses
{
    public class ElectrolyticTokenResponse
    {
        public long Total { get; set; }
        public ICollection<ElectrolyticToken> Data { get; set; }
    }
}
