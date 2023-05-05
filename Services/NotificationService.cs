﻿using BecaworkService.Interfaces;
using BecaworkService.Models;
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
        public async Task<IEnumerable<Notification>> GetNotifications(int page, int pageSize)
        {
            if (pageSize == 0)
            {
                pageSize = 50;
            }
            var notifications = _context.Notifications.ToList().Skip((page - 1) * pageSize).Take(pageSize);
            return notifications;
        }
        public async Task<IEnumerable<Notification>> GetNotifications2(QueryParams queryParams)
        {
            var totalItem = _context.Notifications.Count();
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
                        notifications = _context.Notifications
                            .Where(x => x.Id.ToString().Contains(queryParams.Content)
                            || x.Type.Contains(queryParams.Content)
                            || x.Email.Contains(queryParams.Content)
                            || x.From.Contains(queryParams.Content)
                            ).OrderBy(columnsMap[queryParams.SortBy.ToLower()]).ToList();
                    }
                    else
                    {
                        //false
                        notifications = _context.Notifications
                              .Where(x => x.Id.ToString().Contains(queryParams.Content)
                              || x.Type.Contains(queryParams.Content)
                              || x.Email.Contains(queryParams.Content)
                              || x.From.Contains(queryParams.Content)
                              ).OrderByDescending(columnsMap[queryParams.SortBy.ToLower()]).ToList();
                    }
                }
                else //no content
                {
                    if (queryParams.IsSortAscending)
                    {
                        notifications = _context.Notifications.OrderBy(columnsMap[queryParams.SortBy.ToLower()]).ToList();
                    }
                    else
                    {
                        notifications = _context.Notifications.OrderByDescending(columnsMap[queryParams.SortBy.ToLower()]).ToList();
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
                        notifications = _context.Notifications
                            .Where(x => (x.Id.ToString().Contains(queryParams.Content)
                            || x.Type.Contains(queryParams.Content)
                            || x.Email.Contains(queryParams.Content)
                            || x.From.Contains(queryParams.Content))
                            && ((x.CreatedTime <= queryParams.ToDate && x.CreatedTime >= queryParams.FromDate)
                             || (x.LastModified <= queryParams.ToDate && x.LastModified >= queryParams.FromDate))
                            ).OrderBy(columnsMap[queryParams.SortBy.ToLower()]).ToList();
                    }
                    else
                    {
                        //false
                        notifications = _context.Notifications
                              .Where(x => (x.Id.ToString().Contains(queryParams.Content)
                              || x.Type.Contains(queryParams.Content)
                              || x.Email.Contains(queryParams.Content)
                              || x.From.Contains(queryParams.Content))
                              && ((x.CreatedTime <= queryParams.ToDate && x.CreatedTime >= queryParams.FromDate)
                             || (x.LastModified <= queryParams.ToDate && x.LastModified >= queryParams.FromDate))

                              ).OrderByDescending(columnsMap[queryParams.SortBy.ToLower()]).ToList();
                    }
                }
                else //no content
                {
                    if (queryParams.IsSortAscending)
                    {
                        notifications = _context.Notifications
                            .Where(x => (x.CreatedTime <= queryParams.ToDate && x.CreatedTime >= queryParams.FromDate) || (x.LastModified <= queryParams.ToDate && x.LastModified >= queryParams.FromDate))
                            .OrderBy(columnsMap[queryParams.SortBy.ToLower()]).ToList();
                    }
                    else
                    {
                        notifications = _context.Notifications
                           .Where(x => (x.CreatedTime <= queryParams.ToDate && x.CreatedTime >= queryParams.FromDate) || (x.LastModified <= queryParams.ToDate && x.LastModified >= queryParams.FromDate))
                           .OrderByDescending(columnsMap[queryParams.SortBy.ToLower()]).ToList();
                    }
                }
            }
            
            notifications = notifications.Skip((queryParams.Page - 1) * queryParams.PageSize).Take(queryParams.PageSize).ToList();
            return notifications;
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
