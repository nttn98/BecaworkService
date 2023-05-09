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
        /* public async Task<IEnumerable<Mail>> GetMails1(QueryParams queryParams)
         {
             var mails = new List<Mail>();

             var columnsMap = new Dictionary<string, Expression<Func<Mail, object>>>()
             {
                 ["id"] = s => s.ID,
                 ["email"] = s => s.Email,
                 *//*["emailcontent"] = s => s.EmailContent, //*//*
                 ["createby"] = s => s.CreateBy,
                 ["issend"] = s => s.IsSend,
                 *//*["subject"] = s => s.Subject, //*//*
                 ["sentstatus"] = s => s.SentStatus,
                 ["emailcc"] = s => s.EmailCC,
                 ["emailbcc"] = s => s.EmailBCC,
                 ["location"] = s => s.Location,
                 ["mailtype"] = s => s.MailType,
                 ["organizer"] = s => s.Organizer,
                 ["organizermail"] = s => s.OrganizerMail,
                 ["createtime"] = s => s.CreateTime
             };

             if (queryParams.SortBy == null || !columnsMap.ContainsKey(queryParams.SortBy.ToLower()))
             {
                 queryParams.SortBy = "createtime";
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
                         mails = _context.Mails
                            .Where(x => x.Email.Contains(queryParams.Content)
                            || x.ID.ToString().Contains(queryParams.Content)
                            || x.CreateBy.Contains(queryParams.Content)
                            || x.EmailCC.Contains(queryParams.Content)
                            || x.EmailBCC.Contains(queryParams.Content)
                            || x.Location.Contains(queryParams.Content)
                            || x.MailType.ToString().Contains(queryParams.Content)
                            || x.Organizer.Contains(queryParams.Content)
                            || x.OrganizerMail.Contains(queryParams.Content)
                            || x.UID.Contains(queryParams.Content)
                            )
                            .OrderBy(columnsMap[queryParams.SortBy.ToLower()]).ToList();
                     }
                     else
                     {
                         // false
                         mails = _context.Mails
                            .Where(x => x.Email.Contains(queryParams.Content)
                            || x.ID.ToString().Contains(queryParams.Content)
                            || x.CreateBy.Contains(queryParams.Content)
                            || x.EmailCC.Contains(queryParams.Content)
                            || x.EmailBCC.Contains(queryParams.Content)
                            || x.Location.Contains(queryParams.Content)
                            || x.MailType.ToString().Contains(queryParams.Content)
                            || x.Organizer.Contains(queryParams.Content)
                            || x.OrganizerMail.Contains(queryParams.Content)
                            || x.UID.Contains(queryParams.Content)
                            )
                              .OrderByDescending(columnsMap[queryParams.SortBy.ToLower()]).ToList();
                     }
                 }
                 else // no content
                 {
                     if (queryParams.IsSortAscending)
                     {
                         mails = _context.Mails.OrderBy(columnsMap[queryParams.SortBy.ToLower()]).ToList();
                     }
                     else
                     {
                         mails = _context.Mails.OrderByDescending(columnsMap[queryParams.SortBy.ToLower()]).ToList();
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
                         mails = _context.Mails
                            .Where(x => (x.Email.Contains(queryParams.Content)
                            || x.ID.ToString().Contains(queryParams.Content)
                            || x.CreateBy.Contains(queryParams.Content)
                            || x.EmailCC.Contains(queryParams.Content)
                            || x.EmailBCC.Contains(queryParams.Content)
                            || x.Location.Contains(queryParams.Content)
                            || x.MailType.ToString().Contains(queryParams.Content)
                            || x.Organizer.Contains(queryParams.Content)
                            || x.OrganizerMail.Contains(queryParams.Content)
                            || x.UID.Contains(queryParams.Content))
                            && (x.CreateTime <= queryParams.ToDate && x.CreateTime >= queryParams.FromDate)
                            )
                            .OrderBy(columnsMap[queryParams.SortBy.ToLower()]).ToList();
                     }
                     else
                     {
                         // false
                         mails = _context.Mails
                             .Where(x => (x.Email.Contains(queryParams.Content)
                            || x.ID.ToString().Contains(queryParams.Content)
                            || x.CreateBy.Contains(queryParams.Content)
                            || x.EmailCC.Contains(queryParams.Content)
                            || x.EmailBCC.Contains(queryParams.Content)
                            || x.Location.Contains(queryParams.Content)
                            || x.MailType.ToString().Contains(queryParams.Content)
                            || x.Organizer.Contains(queryParams.Content)
                            || x.OrganizerMail.Contains(queryParams.Content)
                            || x.UID.Contains(queryParams.Content))
                              && (x.CreateTime <= queryParams.ToDate && x.CreateTime >= queryParams.FromDate)
                              )
                              .OrderByDescending(columnsMap[queryParams.SortBy.ToLower()]).ToList();
                     }
                 }
                 else // no content
                 {
                     if (queryParams.IsSortAscending)
                     {
                         //true
                         mails = _context.Mails.Where(x => x.CreateTime <= queryParams.ToDate && x.CreateTime >= queryParams.FromDate).OrderBy(columnsMap[queryParams.SortBy.ToLower()]).ToList();
                     }
                     else
                     {
                         //false
                         mails = _context.Mails.Where(x => x.CreateTime <= queryParams.ToDate && x.CreateTime >= queryParams.FromDate).OrderByDescending(columnsMap[queryParams.SortBy.ToLower()]).ToList();

                     }
                 }
             }
             mails = mails.Skip((queryParams.Page - 1) * queryParams.PageSize).Take(queryParams.PageSize).ToList();
             return mails;
         }*/

        public async Task<QueryResult<Mail>> GetMails2(QueryParams queryParams)
        {
            var connectionString = "Data Source=180.148.1.178,1577;Initial Catalog=CO3.Service;Persist Security Info=True;TrustServerCertificate=True;User ID=thuctap;Password=vntt@123";
            var result = new QueryResult<Mail>();
            if (queryParams.Page == 0)
            {
                queryParams.Page = 1;
            }
            var pagingSpecification = new PagingSpecification(queryParams);
            using (var uniOfWork = new UnitOfWork(connectionString))
            {
                var columnsMap = new Dictionary<string, Expression<Func<Mail, object>>>()
                {
                    ["id"] = s => s.ID,
                    ["email"] = s => s.Email,
                    //["emailcontent"] = s => s.EmailContent,
                    ["createby"] = s => s.CreateBy,
                    ["createtime"] = s => s.CreateTime,
                    ["issend"] = s => s.IsSend,
                    //["subject"] = s => s.Subject, 
                    ["sentstatus"] = s => s.SentStatus,
                    ["emailcc"] = s => s.EmailCC,
                    /*["emailbcc"] = s => s.EmailBCC,*/
                    ["location"] = s => s.Location,
                    ["mailtype"] = s => s.MailType,
                    /*["organizer"] = s => s.Organizer,
                    ["organizermail"] = s => s.OrganizerMail,*/
                    ["uid"] = s => s.UID
                };

                var tempMail = await uniOfWork.MailRepository
                    .FindAll(predicate: x => ((queryParams.FromDate == null || queryParams.ToDate == null)
                    || x.CreateTime >= queryParams.FromDate && x.CreateTime <= queryParams.ToDate)
                    && ((String.IsNullOrEmpty(queryParams.Content)
                    || (EF.Functions.Like(x.Email, $"%{queryParams.Content}%")
                    || EF.Functions.Like(x.CreateBy, $"%{queryParams.Content}%")
                    || EF.Functions.Like(x.Subject, $"%{queryParams.Content}%")
                    || EF.Functions.Like(x.EmailCC, $"%{queryParams.Content}%")
                    || EF.Functions.Like(x.Location, $"%{queryParams.Content}%")
                    || EF.Functions.Like(x.Organizer, $"%{queryParams.Content}%")
                    || EF.Functions.Like(x.UID, $"%{queryParams.Content}%")))),
                    include: null,
                    orderBy: source => (String.IsNullOrEmpty(queryParams.SortBy) || !columnsMap.ContainsKey(queryParams.SortBy.ToLower())) 
                                                                                ? source.OrderBy(d => d.CreateTime)
                                                                                : queryParams.IsSortAscending
                                                                                ? source.OrderBy(columnsMap[queryParams.SortBy])
                                                                                : source.OrderByDescending(columnsMap[queryParams.SortBy]),
                    disableTracking: true,
                    pagingSpecification: pagingSpecification);
                result = tempMail;
            }
            return result;
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
    }
}
