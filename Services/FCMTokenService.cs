using BecaworkService.Helper;
using BecaworkService.Interfaces;
using BecaworkService.Models;
using BecaworkService.Models.Responses;
using BecaworkService.Respository;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BecaworkService.Services
{
    public class FCMTokenService : IFCMTokenService
    {
        private readonly BecaworkDbContext _context;

        public FCMTokenService(BecaworkDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<FCMToken> AddFCMToken(FCMToken objFCMToken)
        {
            _context.FCMTokens.Add(objFCMToken);
            await _context.SaveChangesAsync();
            return objFCMToken;
        }

        public bool DeteleFCMToken(long ID)
        {
            bool result = false;
            var tempFCMToken = _context.FCMTokens.Find(ID);
            if (tempFCMToken != null)
            {
                _context.Entry(tempFCMToken).State = EntityState.Deleted;
                _context.SaveChanges();
                result = true;
            }
            else
            {
                result = false;
            }

            return result;
        }

        public async Task<FCMToken> GetFCMTokenByID(long ID)
        {
            var tempFCMToken = await _context.FCMTokens.FindAsync(ID);
            return tempFCMToken;
        }

        /*public async Task<IEnumerable<FCMToken>> GetFCMTokens(int page, int pageSize)
        {
            var FCMTokens = new List<FCMToken>();

            if (page == 0 && pageSize == 0 || pageSize == 0)
            {
                FCMTokens = await _context.FCMTokens.ToListAsync();
            }
            else if (page == 0)
            {
                FCMTokens = (List<FCMToken>)_context.FCMTokens.ToList().Take(pageSize);
            }
            else
            {
                FCMTokens = (List<FCMToken>)_context.FCMTokens.ToList().Skip((page - 1) * pageSize).Take(pageSize);
            }

            return FCMTokens;
        }*/

        public async Task<QueryResult<FCMToken>> GetFCMTokens(QueryParams queryParams)
        {
            var connectionString = "Data Source=180.148.1.178,1577;Initial Catalog=CO3.Service;Persist Security Info=True;TrustServerCertificate=True;User ID=thuctap;Password=vntt@123";
            var result = new QueryResult<FCMToken>();
            if (queryParams.Page == 0)
            {
                queryParams.Page = 1;
            }
            var pagingSpecification = new PagingSpecification(queryParams);
            using (var uniOfwork = new UnitOfWork(connectionString))
            {
                var columnsMap = new Dictionary<string, Expression<Func<FCMToken, object>>>()
                {
                    ["id"] = s => s.Id,
                    ["email"] = s => s.Email,
                    ["token"] = s => s.Token,
                    ["lastmodified"] = s => s.LastModified,
                    ["createdtime"] = s => s.CreatedTime
                };
                var tempFCMToken = await uniOfwork.FCMTokenRepository
                    .FindAll(predicate: x =>
                    ((queryParams.FromDate == null || queryParams.ToDate == null)
                    || (x.CreatedTime >= queryParams.FromDate && x.CreatedTime <= queryParams.ToDate
                    || x.LastModified >= queryParams.FromDate && x.LastModified <= queryParams.ToDate)
                     && ((string.IsNullOrEmpty(queryParams.Content)
                        || (EF.Functions.Like(x.Id.ToString(), $"%{queryParams.Content}%")
                        || EF.Functions.Like(x.Token, $"%{queryParams.Content}%")
                        || EF.Functions.Like(x.Email, $"%{queryParams.Content}%"))))),
                    include: null,

                    orderBy: source => (String.IsNullOrEmpty(queryParams.SortBy) || !columnsMap.ContainsKey(queryParams.SortBy.ToLower()))
                                                                                ? source.OrderBy(d => d.CreatedTime)
                                                                                : queryParams.IsSortAscending
                                                                                ? source.OrderBy(columnsMap[queryParams.SortBy])
                                                                                : source.OrderByDescending(columnsMap[queryParams.SortBy]),
                    disableTracking: true,
                    pagingSpecification: pagingSpecification);
                result = tempFCMToken;
            }
            return result;
        }

        public async Task<FCMToken> UpdateFCMToken(FCMToken objFCMToken)
        {
            _context.Entry(objFCMToken).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return objFCMToken;
        }
    }
}