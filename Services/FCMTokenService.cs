using BecaworkService.Interfaces;
using BecaworkService.Models;
using BecaworkService.Respository;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BecaworkService.Services
{
    public class FCMTokenService : IFCMTokenService
    {
        private readonly BecaworkDbContext _context;

        public FCMTokenService(BecaworkDbContext context)
        {
            _context = context;
        }

        public async Task<FCMToken> AddFCMToken(FCMToken fcmToken)
        {
            _context.FCMTokens.Add(fcmToken);
            await _context.SaveChangesAsync();
            return fcmToken;
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

        public async Task<IEnumerable<FCMToken>> GetFCMTokens(int page, int pageSize)
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
        }

        public async Task<FCMToken> UpdateFCMToken(FCMToken fcmToken)
        {
            _context.Entry(fcmToken).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return fcmToken;
        }
    }
}