using BecaworkService.Interfaces;
using BecaworkService.Models;
using BecaworkService.Respository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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
        public async Task<IEnumerable<Notification>> GetNotifications()
        {
            return await _context.Notifications.ToListAsync();
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
