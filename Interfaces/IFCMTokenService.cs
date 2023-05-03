using BecaworkService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BecaworkService.Interfaces
{
    public interface IFCMTokenService
    {
        Task<IEnumerable<FCMToken>> GetFCMTokens(int page, int pageSize);
        Task<FCMToken> GetFCMTokenByID(long ID);
        Task<FCMToken> AddFCMToken(FCMToken objFCMToken);
        Task<FCMToken> UpdateFCMToken(FCMToken objFCMToken);
        bool DeteleFCMToken(long ID);
    }
}
