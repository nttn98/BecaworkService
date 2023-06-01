using BecaworkService.Models;
using BecaworkService.Respository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Linq;
using BecaworkService.Helper;
using BecaworkService.Models.Responses;
using System.Linq.Expressions;
using BecaworkService.Interfaces;
using Microsoft.Extensions.Configuration;

namespace BecaworkService.Services
{
    public class ElectrolyticTokenLogService : IElectrolyticTokenLogService
    {
        private readonly BecaworkDbContext _context;
        private readonly IConfiguration _configuration;

        public ElectrolyticTokenLogService(BecaworkDbContext context, IConfiguration configuration)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public async Task<QueryResult<ElectrolyticTokenLog>> GetElectrolyticTokenLogs(QueryParams queryParams)
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            var result = new QueryResult<ElectrolyticTokenLog>();
            if (queryParams.Page == 0)
            {
                queryParams.Page = 1;
            }
            var pagingSpecification = new PagingSpecification(queryParams);
            using (var uniOfwork = new UnitOfWork(connectionString))
            {
                var columnsMap = new Dictionary<string, Expression<Func<ElectrolyticTokenLog, object>>>()
                {
                    ["id"] = s => s.Id,
                    ["request"] = s => s.Request,
                    ["response"] = s => s.Response,
                    ["statuscode"] = s => s.StatusCode,
                    ["lastmodified"] = s => s.LastModified,
                    ["createdtime"] = s => s.CreatedTime
                };
                var tempETokenLog = await uniOfwork.ElectrolyticTokenLogRepository
                    .FindAll(predicate: x =>
                    ((queryParams.FromDate == null || queryParams.ToDate == null)
                    || (x.CreatedTime >= queryParams.FromDate && x.CreatedTime <= queryParams.ToDate
                    || x.LastModified >= queryParams.FromDate && x.LastModified <= queryParams.ToDate)
                     && ((string.IsNullOrEmpty(queryParams.Content)
                        || (EF.Functions.Like(x.Id.ToString(), $"%{queryParams.Content}%")
                        || EF.Functions.Like(x.Request, $"%{queryParams.Content}%")
                        || EF.Functions.Like(x.Response, $"%{queryParams.Content}%")
                        || EF.Functions.Like(x.StatusCode.ToString(), $"%{queryParams.Content}%"))))),

                    include: null,

                    orderBy: source => (String.IsNullOrEmpty(queryParams.SortBy) || !columnsMap.ContainsKey(queryParams.SortBy.ToLower()))
                                                                                ? source.OrderBy(d => d.CreatedTime)
                                                                                : queryParams.IsSortAscending
                                                                                ? source.OrderBy(columnsMap[queryParams.SortBy])
                                                                                : source.OrderByDescending(columnsMap[queryParams.SortBy]),
                    disableTracking: true,
                    pagingSpecification: pagingSpecification);
                result = tempETokenLog;
            }
            return result;
        }

        public async Task<ElectrolyticTokenLog> GetElectrolyticTokenLogByID(long ID)
        {
            var tempETokenLog = await _context.ElectrolyticTokenLogs.FindAsync(ID);
            return tempETokenLog;
        }

        public async Task<ElectrolyticTokenLog> AddElectrolyticTokenLog(ElectrolyticTokenLog objETokenLog)
        {
            _context.ElectrolyticTokenLogs.Add(objETokenLog);
            await _context.SaveChangesAsync();
            return objETokenLog;
        }

        public async Task<ElectrolyticTokenLog> UpdateElectrolyticTokenLog(ElectrolyticTokenLog objETokenLog)
        {
            _context.Entry(objETokenLog).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return objETokenLog;
        }

        public bool DeleteElectrolyticTokenLog(long ID)
        {
            bool result = false;
            var tempETokenLog = _context.ElectrolyticTokenLogs.Find(ID);
            if (tempETokenLog != null)
            {
                _context.Entry(tempETokenLog).State = EntityState.Deleted;
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

