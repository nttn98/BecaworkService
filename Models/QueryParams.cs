﻿namespace BecaworkService.Models
{
    public class QueryParams
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public string SortBy { get; set; }
        public bool IsSortAscending { get; set; }
        public string Content { get; set; }

    }
}
