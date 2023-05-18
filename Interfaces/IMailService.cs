using BecaworkService.Models;
using BecaworkService.Models.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BecaworkService.Interfaces
{
    public interface IMailService
    {
        //Task<IEnumerable<Mail>> GetMails(int page, int pageSize);
        /*Task<IEnumerable<Mail>> GetMails1(QueryParams queryParams);*/
        Task<QueryResult<Mail>> GetMails(QueryParams queryParams);
        Task<Mail> GetMailByID(long ID);
        Task<Mail> AddMail(Mail objMail);
        Task<Mail> UpdateMail(Mail objMail);
        bool DeteleMail(long ID);
    }
}
