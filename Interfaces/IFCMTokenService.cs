using BecaworkService.Models;
using BecaworkService.Models.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BecaworkService.Interfaces
{
    public interface IFCMTokenService
    {
        Task<QueryResult<FCMToken>> GetFCMTokens(QueryParams queryParams);
        Task<FCMToken> GetFCMTokenByID(long ID);
        Task<FCMToken> AddFCMToken(FCMToken objFCMToken);
        Task<FCMToken> UpdateFCMToken(FCMToken objFCMToken);
        bool DeteleFCMToken(long ID);
    }
}
