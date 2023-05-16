﻿using BecaworkService.Helper;
using BecaworkService.Interfaces;
using BecaworkService.Models;
using BecaworkService.Models.Responses;
using BecaworkService.Respository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BecaworkService.Services
{
    public class ElectrolyticTokenService : IElectrolyticTokenService
    {
        private readonly BecaworkDbContext _context;

        public ElectrolyticTokenService(BecaworkDbContext context)
        {
            _context = context;
        }

        public async Task<ElectrolyticToken> AddElectrolyticToken(ElectrolyticToken objElectrolyticToken)
        {
            _context.ElectrolyticTokens.Add(objElectrolyticToken);
            await _context.SaveChangesAsync();
            return objElectrolyticToken;
        }

        public bool DeteleElectrolyticToken(long ID)
        {
            bool result = false;
            var EToken = _context.ElectrolyticTokens.Find(ID);

            if (EToken != null)
            {
                _context.Entry(EToken).State = EntityState.Deleted;
                _context.SaveChanges();
                result = true;
            }
            else
            {
                result = false;
            }

            return result;
        }

        public async Task<ElectrolyticToken> GetElectrolyticTokenByID(long ID)
        {
            var tempEToken = await _context.ElectrolyticTokens.FindAsync(ID);
            return tempEToken;
        }

        public async Task<IEnumerable<ElectrolyticToken>> GetElectrolyticTokens(int page, int pageSize)
        {
            var ETokens = new List<ElectrolyticToken>();

            if (page == 0 && pageSize == 0 || pageSize == 0)
            {
                ETokens = await _context.ElectrolyticTokens.ToListAsync();
            }
            else if (page == 0)
            {
                ETokens = (List<ElectrolyticToken>)_context.ElectrolyticTokens.ToList().Take(pageSize);
            }
            else
            {
                ETokens = (List<ElectrolyticToken>)_context.ElectrolyticTokens.ToList().Skip((page - 1) * pageSize).Take(pageSize);
            }

            return ETokens;
        }

        public async Task<QueryResult<ElectrolyticToken>> GetElectrolyticTokens2(QueryParams queryParams)
        {
            var connectionString = "Data Source=180.148.1.178,1577;Initial Catalog=CO3.Service;Persist Security Info=True;TrustServerCertificate=True;User ID=thuctap;Password=vntt@123";
            var result = new QueryResult<ElectrolyticToken>();
            if (queryParams.Page == 0)
            {
                queryParams.Page = 1;
            }
            var pagingSpecification = new PagingSpecification(queryParams);
            using (var uniOfwork = new UnitOfWork(connectionString))
            {
                var columnsMap = new Dictionary<string, Expression<Func<ElectrolyticToken, object>>>()
                {
                    ["id"] = s => s.Id,
                    ["mail"] = s => s.Mail,
                    ["request"] = s => s.Request,
                    ["response"] = s => s.Response,
                    ["lastmodified"] = s => s.LastModified,
                    ["createdtime"] = s => s.CreatedTime
                };
                var tempElectrolyticToken = await uniOfwork.ElectrolyticTokenRepository
                    .FindAll(predicate: x =>
                    ((queryParams.FromDate == null || queryParams.ToDate == null)
                    || (x.CreatedTime >= queryParams.FromDate && x.CreatedTime <= queryParams.ToDate
                    || x.LastModified >= queryParams.FromDate && x.LastModified <= queryParams.ToDate)
                     && ((string.IsNullOrEmpty(queryParams.Content)
                        || (EF.Functions.Like(x.Id.ToString(), $"%{queryParams.Content}%")
                        || EF.Functions.Like(x.Request, $"%{queryParams.Content}%")
                        || EF.Functions.Like(x.Response, $"%{queryParams.Content}%")
                        || EF.Functions.Like(x.Mail.ToString(), $"%{queryParams.Content}%"))))),
                    include: null,

                    orderBy: source => (String.IsNullOrEmpty(queryParams.SortBy) || !columnsMap.ContainsKey(queryParams.SortBy.ToLower()))
                                                                                ? source.OrderBy(d => d.CreatedTime)
                                                                                : queryParams.IsSortAscending
                                                                                ? source.OrderBy(columnsMap[queryParams.SortBy])
                                                                                : source.OrderByDescending(columnsMap[queryParams.SortBy]),
                    disableTracking: true,
                    pagingSpecification: pagingSpecification);
                result = tempElectrolyticToken;
            }
            return result;
        }

        public async Task<ElectrolyticToken> UpdateElectrolyticToken(ElectrolyticToken objElectrolyticToken)
        {
            _context.Entry(objElectrolyticToken).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return objElectrolyticToken;
        }
    }
}
