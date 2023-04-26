using BecaworkService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BecaworkService.Interfaces
{
    public interface IMailService
    {
        Task<IEnumerable<Mail>> GetMails(int page, int pageSize);
        Task<IEnumerable<Mail>> GetMails2(QueryParams queryParams);
        Task<Mail> GetMailByID(long ID);
        Task<Mail> AddMail(Mail objMail);
        Task<Mail> UpdateMail(Mail objMail);
        bool DeteleMail(long ID);

    }
}
