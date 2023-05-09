using BecaworkService.Models;

namespace BecaworkService.Helper
{
    public class PagingSpecification
    {
        public PagingSpecification(QueryParams queryObj)
        {
            if (queryObj.Page == 0)
            {
                this.Skip = 0;
                if (queryObj.PageSize <= 0)
                {
                    this.IsTakeAll = true;
                }
                else
                {
                    this.Take = queryObj.PageSize;
                }
            }
            else
            {
                if (queryObj.PageSize <= 0)
                {
                    this.Take = 20;
                }
                else
                {
                    this.Take = queryObj.PageSize;
                }

                if (queryObj.Page <= 0)
                {
                    this.Skip = 0;
                }
                else
                {
                    this.Skip = (queryObj.Page - 1) * this.Take;
                }
            }
        }

        public PagingSpecification(int skip, int take)
        {
            this.Skip = skip < 0 ? 0 : skip;
            this.Take = take < 0 ? 10 : take;
        }

        public int Skip { get; set; }
        public int Take { get; set; }
        public bool IsTakeAll { get; set; }
    }
}
