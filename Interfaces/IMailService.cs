using BecaworkService.Models;
using BecaworkService.Models.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BecaworkService.Interfaces
{
    public interface IMailService
    {
        Task<QueryResult<Mail>> GetMails(QueryParams queryParams);
        Task<Mail> GetMailByID(long ID);
        Task<Mail> AddMail(Mail objMail);
        Task<Mail> UpdateMail(Mail objMail);
        bool DeteleMail(long ID);
        Task<bool> SendMailBySMTP(long ID);

    }
}
