using BecaworkService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BecaworkService.Interfaces
{
    public interface IElectrolyticTokenLogService
    {
        Task<IEnumerable<ElectrolyticTokenLog>> GetElectrolyticTokenLogs(int page, int pageSize);
        Task<ElectrolyticTokenLog> GetElectrolyticTokenLogByID(long ID);
        /* Task<FCMTokenLog> AddFCMTokenLog(FCMTokenLog objFCMTokenLog);*/
        Task<ElectrolyticTokenLog> UpdateElectrolyticTokenLog(ElectrolyticTokenLog objElectrolyticTokenLog);
        bool DeleteElectrolyticTokenLog(long ID);
    }
}
