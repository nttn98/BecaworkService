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
    public class MailService : IMailService
    {
        private readonly BecaworkDbContext _context;

        public MailService(BecaworkDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<IEnumerable<Mail>> GetMails(int page, int pageSize)
        {
            var mails = _context.Mails.ToList().Skip((page - 1) * pageSize).Take(pageSize);
            return mails;

        }
        public async Task<Mail> GetMailByID(long ID)
        {
            var tempMail = await _context.Mails.FindAsync(ID);
            return tempMail;
        }
        public async Task<Mail> AddMail(Mail objMail)
        {
            _context.Mails.Add(objMail);
            await _context.SaveChangesAsync();
            return objMail;
        }

        public async Task<Mail> UpdateMail(Mail objMail)
        {
            _context.Entry(objMail).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return objMail;
        }
        public bool DeteleMail(long ID)
        {
            bool result = false;
            var tempMail = _context.Mails.Find(ID);
            if (tempMail != null)
            {
                _context.Entry(tempMail).State = EntityState.Deleted;
                _context.SaveChanges();
                result = true;
            }
            else
            {
                result = false;
            }
            return result;
        }

        public Task<IEnumerable<Mail>> GetMails()
        {
            throw new NotImplementedException();
        }
    }
}
