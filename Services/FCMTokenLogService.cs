using BecaworkService.Interfaces;
using BecaworkService.Models;
using BecaworkService.Respository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BecaworkService.Services
{
    public class FCMTokenLogService : IFCMTokenLogService
    {
        private readonly BecaworkDbContext _context;

        public FCMTokenLogService(BecaworkDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<IEnumerable<FCMTokenLog>> GetFCMTokenLogs(int page, int pageSize)
        {
            var FCMTokenLogs = new List<FCMTokenLog>();

            if (page == 0 && pageSize == 0 || pageSize == 0)
            {
                FCMTokenLogs = await _context.FCMTokenLogs.ToListAsync();
            }
            else if (page == 0)
            {
                FCMTokenLogs = (List<FCMTokenLog>)_context.FCMTokenLogs.ToList().Take(pageSize);
            }
            else
            {
                FCMTokenLogs = (List<FCMTokenLog>)_context.FCMTokenLogs.ToList().Skip((page - 1) * pageSize).Take(pageSize);
            }

            return FCMTokenLogs;
        }

        public async Task<FCMTokenLog> GetFCMTokenLogByID(long ID)
        {
            var tempGetFCMTokenLog = await _context.FCMTokenLogs.FindAsync(ID);
            return tempGetFCMTokenLog;
        }

    /*    public async Task<FCMTokenLog> AddFCMTokenLog(FCMTokenLog objFCMTokenLog)
        {
            _context.FCMTokenLogs.Add(objFCMTokenLog);
            await _context.SaveChangesAsync();
            return objFCMTokenLog;
        }*/



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
