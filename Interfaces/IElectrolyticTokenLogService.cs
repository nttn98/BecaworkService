using BecaworkService.Models;
using BecaworkService.Models.Responses;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BecaworkService.Interfaces
{
    public interface IElectrolyticTokenLogService
    {
        Task<IEnumerable<ElectrolyticTokenLog>> GetElectrolyticTokenLogs(int page, int pageSize);
        Task<QueryResult<ElectrolyticTokenLog>> GetElectrolyticTokenLogs2([FromQuery] QueryParams queryParams);
        Task<ElectrolyticTokenLog> GetElectrolyticTokenLogByID(long ID);
        Task<ElectrolyticTokenLog> AddElectrolyticTokenLog(ElectrolyticTokenLog objElectrolyticTokenLog);
        Task<ElectrolyticTokenLog> UpdateElectrolyticTokenLog(ElectrolyticTokenLog objElectrolyticTokenLog);
        bool DeleteElectrolyticTokenLog(long ID);
    }
}
