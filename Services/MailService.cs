using BecaworkService.Interfaces;
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
    public class MailService : IMailService
    {
        private readonly BecaworkDbContext _context;

        public MailService(BecaworkDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<IEnumerable<Mail>> GetMails(int page, int pageSize)
        {
            if (page == 0 && pageSize == 0 || pageSize == 0)
            {
                var mails = await _context.Mails.ToListAsync();
                return mails;
            }
            else
            {
                var mails = _context.Mails.ToList().Skip((page - 1) * pageSize).Take(pageSize);
                return mails;
            }

        }

        public async Task<IEnumerable<Mail>> GetMails2(QueryParams queryParams)
        {
            var columnsMap = new Dictionary<string, Expression<Func<Mail, object>>>()
            {
                ["id"] = s => s.ID,
                ["email"] = s => s.Email,
                ["emailContent"] = s => s.EmailContent,
                ["fileattach"] = s => s.FileAttach,
                ["createby"] = s => s.CreateBy,
                ["createTime"] = s => s.CreateTime,
                ["issend"] = s => s.IsSend,
                ["sendtime"] = s => s.SendTime,
                ["subject"] = s => s.Subject,
                ["sentstatus"] = s => s.SentStatus,
                ["emailcc"] = s => s.EmailCC,
                ["emailbcc"] = s => s.EmailBCC,
                ["fromdate"] = s => s.FromDate,
                ["todate"] = s => s.ToDate,
                ["location"] = s => s.Location,
                ["mailtype"] = s => s.MailType,
                ["organizer"] = s => s.Organizer,
                ["organizermail"] = s => s.OrganizerMail,
                ["uid"] = s => s.UID
            };

            var file = _context.Mails
                .Where(x => !string.IsNullOrEmpty(queryParams.Content) || (x.Subject.Contains(queryParams.Content) || x.SentStatus.Contains(queryParams.Content)))
                .OrderBy(columnsMap[queryParams.SortBy.ToLower()]).ToList();

            if (queryParams.Page == 0 && queryParams.PageSize == 0 || queryParams.PageSize == 0)
            {
                return file;
            }
            else
            {
                file = file.ToList().Skip((queryParams.Page - 1) * queryParams.PageSize).Take(queryParams.PageSize).ToList();
                return file;
            }

        }



        /* public async Task<IEnumerable<Mail>> GetMails2(QueryParams queryParams)
         {
             var mails = new List<Mail>();

             if (!string.IsNullOrEmpty(queryParams.Content))
             {
                 mails = await _context.Mails.Where(x => x.Subject.Contains(queryParams.Content)).ToListAsync();
             }
             else
             {
                 mails = await _context.Mails.ToListAsync();
             }

             if (queryParams.SortBy == "CreateBy")
             {
                 if (queryParams.IsSortAscending)
                 {
                     mails = mails.OrderBy(x => x.CreateBy).ToList();

                 }
                 else
                 {
                     mails = mails.OrderByDescending(x => x.CreateBy).ToList();
                 }
             }
             else
             {
                 mails = await _context.Mails.ToListAsync();
             }

             if (queryParams.Page == 0 && queryParams.PageSize == 0 || queryParams.PageSize == 0)
             {
                 return mails;
             }
             else
             {
                 mails = mails.ToList().Skip((queryParams.Page - 1) * queryParams.PageSize).Take(queryParams.PageSize).ToList();
                 return mails;
             }
         }*/

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
    }
}
