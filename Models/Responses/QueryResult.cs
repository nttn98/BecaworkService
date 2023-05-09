using System.Collections.Generic;

namespace BecaworkService.Models.Responses
{
    public class QueryResult<T>
    {
        public long TotalItems { get; set; }
        public IEnumerable<T> Items { get; set; }
    }
}
