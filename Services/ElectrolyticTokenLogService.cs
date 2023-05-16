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

namespace BecaworkService.Services
{
    public class ElectrolyticTokenLogService : IElectrolyticTokenLogService
    {
        private readonly BecaworkDbContext _context;

        public ElectrolyticTokenLogService(BecaworkDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<IEnumerable<ElectrolyticTokenLog>> GetElectrolyticTokenLogs(int page, int pageSize)
        {
            var ETokenLog = new List<ElectrolyticTokenLog>();

            if (page == 0 && pageSize == 0 || pageSize == 0)
            {
                ETokenLog = await _context.ElectrolyticTokenLogs.ToListAsync();
                return ETokenLog;
            }
            else if (page == 0)
            {
                ETokenLog = (List<ElectrolyticTokenLog>)_context.ElectrolyticTokenLogs.ToList().Take(pageSize);
                return ETokenLog;
            }
            else
            {
                ETokenLog = (List<ElectrolyticTokenLog>)_context.FCMTokenLogs.ToList().Skip((page - 1) * pageSize).Take(pageSize);
                return ETokenLog;
            }
        }

        public async Task<QueryResult<ElectrolyticTokenLog>> GetElectrolyticTokenLogs2(QueryParams queryParams)
        {
            var connectionString = "Data Source=180.148.1.178,1577;Initial Catalog=CO3.Service;Persist Security Info=True;TrustServerCertificate=True;User ID=thuctap;Password=vntt@123";
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
                var tempElectrolyticTokenLog = await uniOfwork.ElectrolyticTokenLogRepository
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
                result = tempElectrolyticTokenLog;
            }
            return result;
        }

        public async Task<ElectrolyticTokenLog> GetElectrolyticTokenLogByID(long ID)
        {
            var ETokenLog = await _context.ElectrolyticTokenLogs.FindAsync(ID);
            return ETokenLog;
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

