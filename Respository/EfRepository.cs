﻿using BecaworkService.Helper;
using BecaworkService.Models.Responses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BecaworkService.Respository
{
    public class EfRepository<T> : IAsyncRepository<T> where T : class
    {
        private readonly BecaworkDbContext _context;

        public EfRepository(BecaworkDbContext context)
        {
            _context = context;
        }

        public async Task Add(T entity)
        {
            await _context.AddAsync(entity);
        }

        public async Task AddRange(List<T> entities)
        {
            await _context.Set<T>().AddRangeAsync(entities);
        }

        public void Dispose()
        {
            _context?.Dispose();
        }

        public IQueryable<T> FindAll(Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            bool disableTracking = true, PagingSpecification pagingSpecification = null)
        {
            IQueryable<T> items = _context.Set<T>();
            if (include != null)
            {
                items = include(items);
            }

            if (disableTracking == true)
            {
                items = items.AsNoTracking();
            }

            if (orderBy != null)
            {
                items = orderBy(items);
            }

            if (pagingSpecification != null && !pagingSpecification.IsTakeAll)
            {
                items = items.Skip(pagingSpecification.Skip).Take(pagingSpecification.Take);
            }

            return items;
        }

        public async Task<QueryResult<T>> FindAll(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, bool disableTracking = true, PagingSpecification pagingSpecification = null)
        {
            IQueryable<T> items = _context.Set<T>();
            if (include != null)
            {
                items = include(items);
            }

            if (predicate != null)
            {
                items = items.Where(predicate);
            }

            if (disableTracking == true)
            {
                items = items.AsNoTracking();
            }

            if (orderBy != null)
            {
                items = orderBy(items);
            }

            var totalItems = await items.CountAsync();

            if (pagingSpecification != null && !pagingSpecification.IsTakeAll)
            {
                items = items.Skip(pagingSpecification.Skip).Take(pagingSpecification.Take);
            }

            var results = await items.ToListAsync();

            return new QueryResult<T>
            {
                TotalItems = totalItems,
                Items = results
            };
        }

        public async Task<T> FindSingle(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, bool disableTracking = true)
        {
            return await FindAll(include, orderBy, disableTracking).SingleOrDefaultAsync(predicate);
        }

        public async Task<T> FindFirst(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, bool disableTracking = true)
        {
            return await FindAll(include, orderBy, disableTracking).FirstOrDefaultAsync(predicate);
        }

        public async Task<T> FindLast(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, bool disableTracking = true)
        {
            return await FindAll(include, orderBy, disableTracking).LastOrDefaultAsync(predicate);
        }

        public void Remove(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public void RemoveRange(List<T> entities)
        {
            _context.Set<T>().RemoveRange(entities);
        }

        public void Update(T entity)
        {
            _context.Update(entity);
        }

        public void UpdateRange(List<T> entities)
        {
            _context.Set<T>().UpdateRange(entities);
        }

        public async Task<List<T>> FindList(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, bool disableTracking = true)
        {
            return await FindAll(include, orderBy, disableTracking).Where(predicate).ToListAsync();
        }
    }
}
