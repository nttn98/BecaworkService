using BecaworkService.Helper;
using BecaworkService.Interfaces;
using BecaworkService.Models;
using BecaworkService.Models.Responses;
using BecaworkService.Respository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Net.Mail;
using System.Threading.Tasks;

namespace BecaworkService.Services
{
    public class FCMTokenLogService : IFCMTokenLogService
    {
        private readonly BecaworkDbContext _context;
        private readonly IConfiguration _configuration;
        public FCMTokenLogService(BecaworkDbContext context, IConfiguration configuration)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }
        /* public async Task<IEnumerable<FCMTokenLog>> GetFCMTokenLogs2(QueryParams queryParams)
         {
             var tempFCMTokenLogs = new List<FCMTokenLog>();
             var columnsMap = new Dictionary<string, Expression<Func<FCMTokenLog, object>>>()
             {
                 ["id"] = s => s.Id,
                 ["statuscode"] = s => s.StatusCode,
                 ["lastmodified"] = s => s.LastModified,
                 ["createdtime"] = s => s.CreatedTime

             };

             if (queryParams.SortBy == null || !columnsMap.ContainsKey(queryParams.SortBy.ToLower()))
             {
                 queryParams.SortBy = "createdtime";
             }

             if (queryParams.PageSize == 0)
             {
                 queryParams.PageSize = 50;
             }
             if (queryParams.FromDate == null || queryParams.ToDate == null)
             {
                 if (!string.IsNullOrEmpty(queryParams.Content)) // have content
                 {
                     if (queryParams.IsSortAscending)
                     {
                         //true
                         tempFCMTokenLogs = _context.FCMTokenLogs
                             .Where(x => x.Id.ToString().Contains(queryParams.Content)
                             || x.Response.Contains(queryParams.Content)
                             || x.StatusCode.ToString().Contains(queryParams.Content)
                             ).OrderBy(columnsMap[queryParams.SortBy.ToLower()]).ToList();
                     }
                     else
                     {
                         tempFCMTokenLogs = _context.FCMTokenLogs
                             .Where(x => x.Id.ToString().Contains(queryParams.Content)
                             || x.Response.Contains(queryParams.Content)
                             || x.StatusCode.ToString().Contains(queryParams.Content)
                             ).OrderByDescending(columnsMap[queryParams.SortBy.ToLower()]).ToList();
                     }
                 }
                 else // no content
                 {
                     if (queryParams.IsSortAscending)
                     {
                         tempFCMTokenLogs = _context.FCMTokenLogs.OrderBy(columnsMap[queryParams.SortBy.ToLower()]).ToList();
                     }
                     else
                     {
                         tempFCMTokenLogs = _context.FCMTokenLogs.OrderByDescending(columnsMap[queryParams.SortBy.ToLower()]).ToList();

                     }
                 }
             }
             // queryParams.FromDate and queryParams.ToDate "NOT null"
             else
             {
                 if (!string.IsNullOrEmpty(queryParams.Content)) //have content
                 {
                     if (queryParams.IsSortAscending)
                     {
                         //true
                         tempFCMTokenLogs = _context.FCMTokenLogs
                             .Where(x => (x.Id.ToString().Contains(queryParams.Content)
                             || x.Response.Contains(queryParams.Content)
                             || x.StatusCode.ToString().Contains(queryParams.Content))
                             && ((x.CreatedTime <= queryParams.ToDate && x.CreatedTime >= queryParams.FromDate)
                             || (x.LastModified <= queryParams.ToDate && x.LastModified >= queryParams.FromDate))
                             ).OrderBy(columnsMap[queryParams.SortBy.ToLower()]).ToList();
                     }
                     else
                     {
                         //false
                         tempFCMTokenLogs = _context.FCMTokenLogs
                            .Where(x => (x.Id.ToString().Contains(queryParams.Content)
                            || x.Response.Contains(queryParams.Content)
                            || x.StatusCode.ToString().Contains(queryParams.Content))
                            && ((x.CreatedTime <= queryParams.ToDate && x.CreatedTime >= queryParams.FromDate)
                            || (x.LastModified <= queryParams.ToDate && x.LastModified >= queryParams.FromDate))
                            ).OrderByDescending(columnsMap[queryParams.SortBy.ToLower()]).ToList();
                     }
                 }
                 else // no content
                 {
                     if (queryParams.IsSortAscending)
                     {
                         tempFCMTokenLogs = _context.FCMTokenLogs
                             .Where(x => (x.CreatedTime <= queryParams.ToDate && x.CreatedTime >= queryParams.FromDate)
                             || (x.LastModified <= queryParams.ToDate && x.LastModified >= queryParams.FromDate)).OrderBy(columnsMap[queryParams.SortBy.ToLower()]).ToList();
                     }
                     else
                     {
                         tempFCMTokenLogs = _context.FCMTokenLogs
                     .Where(x => (x.CreatedTime <= queryParams.ToDate && x.CreatedTime >= queryParams.FromDate)
                     || (x.LastModified <= queryParams.ToDate && x.LastModified >= queryParams.FromDate)).OrderByDescending(columnsMap[queryParams.SortBy.ToLower()]).ToList();
                     }
                 }
             }

             tempFCMTokenLogs = tempFCMTokenLogs.Skip((queryParams.Page - 1) * queryParams.PageSize).Take(queryParams.PageSize).ToList();
             return tempFCMTokenLogs;
         }*/

        public async Task<QueryResult<FCMTokenLog>> GetFCMTokenLogs(QueryParams queryParams)
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            var result = new QueryResult<FCMTokenLog>();
            if (queryParams.Page == 0)
            {
                queryParams.Page = 1;
            }
            var pagingSpecification = new PagingSpecification(queryParams);
            using (var uniOfwork = new UnitOfWork(connectionString))
            {
                var columnsMap = new Dictionary<string, Expression<Func<FCMTokenLog, object>>>()
                {
                    ["id"] = s => s.Id,
                    ["statuscode"] = s => s.StatusCode,
                    ["lastmodified"] = s => s.LastModified,
                    ["createdtime"] = s => s.CreatedTime
                };
                var tempFCMTokenLog = await uniOfwork.FCMTokenLogRepository
                    .FindAll(predicate: x =>
                    ((queryParams.FromDate == null || queryParams.ToDate == null)
                    || (x.CreatedTime >= queryParams.FromDate && x.CreatedTime <= queryParams.ToDate
                    || x.LastModified >= queryParams.FromDate && x.LastModified <= queryParams.ToDate))

                    && ((string.IsNullOrEmpty(queryParams.Content)
                    || (EF.Functions.Like(x.Id.ToString(), $"%{queryParams.Content}%")
                    || EF.Functions.Like(x.StatusCode.ToString(), $"%{queryParams.Content}%")))),
                    include: null,

                    orderBy: source => (String.IsNullOrEmpty(queryParams.SortBy) || !columnsMap.ContainsKey(queryParams.SortBy.ToLower()))
                                                                                ? source.OrderByDescending(d => d.CreatedTime)
                                                                                : queryParams.IsSortAscending
                                                                                ? source.OrderBy(columnsMap[queryParams.SortBy.ToLower()])
                                                                                : source.OrderByDescending(columnsMap[queryParams.SortBy.ToLower()]),
                    disableTracking: true,
                    pagingSpecification: pagingSpecification);
                result = tempFCMTokenLog;
            }
            return result;
        }

        public async Task<FCMTokenLog> GetFCMTokenLogByID(long ID)
        {
            var tempGetFCMTokenLog = await _context.FCMTokenLogs.FindAsync(ID);
            return tempGetFCMTokenLog;
        }

        public async Task<FCMTokenLog> AddFCMTokenLog(FCMTokenLog objFCMTokenLog)
        {
            _context.FCMTokenLogs.Add(objFCMTokenLog);
            await _context.SaveChangesAsync();
            return objFCMTokenLog;
        }

        public async Task<FCMTokenLog> UpdateFCMTokenLog(FCMTokenLog objFCMTokenLog)
        {
            _context.Entry(objFCMTokenLog).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return objFCMTokenLog;
        }

        public bool DeleteFCMTokenLog(long ID)
        {
            bool result = false;
            var tempFCMTokenLog = _context.FCMTokenLogs.Find(ID);
            if (tempFCMTokenLog != null)
            {
                _context.Entry(tempFCMTokenLog).State = EntityState.Deleted;
                _context.SaveChanges();
                result = true;
            }
            else
            {
                result = false;
            }
            return result;
        }

    }
}
