using BecaworkService.Helper;
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
    public class NotificationService : INotificationService
    {
        private readonly BecaworkDbContext _context;

        public NotificationService(BecaworkDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<NotificationResponse> GetNotifications(int page, int pageSize)
        {
            var total = await _context.Notifications.CountAsync();
            if (page == 0 && pageSize == 0)
            {
                var notifications = await _context.Notifications.ToListAsync();
                return new NotificationResponse
                {
                    Total = total,
                    Data = notifications
                };
            }
            else
            {
                var notifications = await _context.Notifications.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
                return new NotificationResponse
                {
                    Total = total,
                    Data = notifications
                };
            }
        }

        /*public async Task<NotificationResponse> GetNotifications1(QueryParams queryParams)
        {
            var total = await _context.Notifications.CountAsync();
            var notifications = new List<Notification>();
            var columnsMap = new Dictionary<string, Expression<Func<Notification, object>>>()
            {
                ["id"] = s => s.Id,
                ["createdtime"] = s => s.CreatedTime,
                ["type"] = s => s.Type,
                ["isread"] = s => s.IsRead,
                ["email"] = s => s.Email,
                ["lastmodified"] = s => s.LastModified,
                ["from"] = s => s.From,
                ["isseen"] = s => s.IsSeen,
            };
            if (queryParams.SortBy == null || !columnsMap.ContainsKey(queryParams.SortBy.ToLower()))
            {
                queryParams.SortBy = "createdtime";
            }
            if (queryParams.Page == 0)
            {
                queryParams.Page = 1;
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
                        // true
                        notifications = await _context.Notifications
                            .Where(x => x.Id.ToString().Contains(queryParams.Content)
                            || x.Type.Contains(queryParams.Content)
                            || x.Email.Contains(queryParams.Content)
                            || x.From.Contains(queryParams.Content)
                            ).OrderBy(columnsMap[queryParams.SortBy.ToLower()]).Skip((queryParams.Page - 1) * queryParams.PageSize).Take(queryParams.PageSize).ToListAsync();
                    }
                    else
                    {
                        //false
                        notifications = await _context.Notifications
                              .Where(x => x.Id.ToString().Contains(queryParams.Content)
                              || x.Type.Contains(queryParams.Content)
                              || x.Email.Contains(queryParams.Content)
                              || x.From.Contains(queryParams.Content)
                              ).OrderByDescending(columnsMap[queryParams.SortBy.ToLower()]).Skip((queryParams.Page - 1) * queryParams.PageSize).Take(queryParams.PageSize).ToListAsync();
                    }
                }
                else //no content
                {
                    if (queryParams.IsSortAscending)
                    {
                        notifications = await _context.Notifications.OrderBy(columnsMap[queryParams.SortBy.ToLower()]).Skip((queryParams.Page - 1) * queryParams.PageSize).Take(queryParams.PageSize).ToListAsync();
                    }
                    else
                    {
                        notifications = await _context.Notifications.OrderByDescending(columnsMap[queryParams.SortBy.ToLower()]).Skip((queryParams.Page - 1) * queryParams.PageSize).Take(queryParams.PageSize).ToListAsync();
                    }
                }
            }
            // queryParams.FromDate and queryParams.ToDate "NOT null"
            else
            {
                if (!string.IsNullOrEmpty(queryParams.Content)) // have content
                {
                    if (queryParams.IsSortAscending)
                    {
                        // true
                        notifications = await _context.Notifications
                            .Where(x => (x.Id.ToString().Contains(queryParams.Content)
                            || x.Type.Contains(queryParams.Content)
                            || x.Email.Contains(queryParams.Content)
                            || x.From.Contains(queryParams.Content))
                            && ((x.CreatedTime <= queryParams.ToDate && x.CreatedTime >= queryParams.FromDate)
                             || (x.LastModified <= queryParams.ToDate && x.LastModified >= queryParams.FromDate))
                            ).OrderBy(columnsMap[queryParams.SortBy.ToLower()]).Skip((queryParams.Page - 1) * queryParams.PageSize).Take(queryParams.PageSize).ToListAsync();
                    }
                    else
                    {
                        //false
                        notifications = await _context.Notifications
                              .Where(x => (x.Id.ToString().Contains(queryParams.Content)
                              || x.Type.Contains(queryParams.Content)
                              || x.Email.Contains(queryParams.Content)
                              || x.From.Contains(queryParams.Content))
                              && ((x.CreatedTime <= queryParams.ToDate && x.CreatedTime >= queryParams.FromDate)
                             || (x.LastModified <= queryParams.ToDate && x.LastModified >= queryParams.FromDate))

                              ).OrderByDescending(columnsMap[queryParams.SortBy.ToLower()]).ToListAsync();
                    }
                }
                else //no content
                {
                    if (queryParams.IsSortAscending)
                    {
                        notifications = await _context.Notifications
                            .Where(x => (x.CreatedTime <= queryParams.ToDate && x.CreatedTime >= queryParams.FromDate) || (x.LastModified <= queryParams.ToDate && x.LastModified >= queryParams.FromDate))
                            .OrderBy(columnsMap[queryParams.SortBy.ToLower()]).Skip((queryParams.Page - 1) * queryParams.PageSize).Take(queryParams.PageSize).ToListAsync();

                    }
                    else
                    {
                        notifications = await _context.Notifications
                           .Where(x => (x.CreatedTime <= queryParams.ToDate && x.CreatedTime >= queryParams.FromDate) || (x.LastModified <= queryParams.ToDate && x.LastModified >= queryParams.FromDate))
                           .OrderByDescending(columnsMap[queryParams.SortBy.ToLower()]).Skip((queryParams.Page - 1) * queryParams.PageSize).Take(queryParams.PageSize).ToListAsync();
                    }
                }
            }
            return new NotificationResponse
            {
                Total = total,
                Data = notifications
            };
        }*/

        public async Task<QueryResult<Notification>> GetNotifications2(QueryParams queryParams)
        {
            var connectionString = "Data Source=180.148.1.178,1577;Initial Catalog=CO3.Service;Persist Security Info=True;TrustServerCertificate=True;User ID=thuctap;Password=vntt@123";
            var result = new QueryResult<Notification>();

            if (queryParams.Page == 0)
            {
                queryParams.Page = 1;
            }
            var pagingSpecification = new PagingSpecification(queryParams);
            using (var unitOfWork = new UnitOfWork(connectionString))
            {
                var columnsMap = new Dictionary<string, Expression<Func<Notification, object>>>()
                {
                    ["id"] = s => s.Id,
                    ["createdtime"] = s => s.CreatedTime,
                    ["type"] = s => s.Type,
                    ["isread"] = s => s.IsRead,
                    ["email"] = s => s.Email,
                    ["lastmodified"] = s => s.LastModified,
                    ["from"] = s => s.From,
                    ["isseen"] = s => s.IsSeen,
                };

                var tempNotification = await unitOfWork.NotificationRepository
                    .FindAll(predicate: x =>
                    ((queryParams.FromDate == null || queryParams.ToDate == null)
                        || x.CreatedTime >= queryParams.FromDate && x.CreatedTime <= queryParams.ToDate
                        || x.LastModified >= queryParams.FromDate && x.LastModified <= queryParams.ToDate)
                    && ((string.IsNullOrEmpty(queryParams.Content)
                        || (EF.Functions.Like(x.Id.ToString(), $"%{queryParams.Content}%")
                        || EF.Functions.Like(x.CreatedTime.ToString(), $"%{queryParams.Content}%")
                        || EF.Functions.Like(x.Type, $"%{queryParams.Content}%")
                        || EF.Functions.Like(x.Content, $"%{queryParams.Content}%")
                        || EF.Functions.Like(x.Email, $"%{queryParams.Content}%")
                        || EF.Functions.Like(x.Type, $"%{queryParams.Content}%")
                        || EF.Functions.Like(x.From, $"%{queryParams.Content}%")
                        || EF.Functions.Like(x.Url, $"%{queryParams.Content}%")))),

                    include: null,
                    orderBy: source => (String.IsNullOrEmpty(queryParams.SortBy) || !columnsMap.ContainsKey(queryParams.SortBy.ToLower()))
                                                                                ? source.OrderBy(d => d.CreatedTime)
                                                                                : queryParams.IsSortAscending
                                                                                ? source.OrderBy(columnsMap[queryParams.SortBy.ToLower()])
                                                                                : source.OrderByDescending(columnsMap[queryParams.SortBy.ToLower()]),
                    disableTracking: true,
                    pagingSpecification: pagingSpecification);
                result = tempNotification;
            }

            return result;
        }

        public async Task<Notification> GetNotificationByID(long ID)
        {
            var tempNotifi = await _context.Notifications.FindAsync(ID);
            return tempNotifi;
        }

        public async Task<Notification> AddNotifi(Notification objNotifi)
        {
            _context.Notifications.Add(objNotifi);
            await _context.SaveChangesAsync();
            return objNotifi;
        }

        public async Task<Notification> UpdateNotifi(Notification objNotifi)
        {
            _context.Entry(objNotifi).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return objNotifi;
        }

        public bool DeleteNotifi(long ID)
        {
            bool result = false;
            var tempNotifi = _context.Notifications.Find(ID);
            if (tempNotifi != null)
            {
                _context.Entry(tempNotifi).State = EntityState.Deleted;
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
