using BecaworkService.Models;
using BecaworkService.Models.Responses;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BecaworkService.Interfaces
{
    public interface IElectrolyticTokenLogService
    {
        Task<QueryResult<ElectrolyticTokenLog>> GetElectrolyticTokenLogs([FromQuery] QueryParams queryParams);
        Task<ElectrolyticTokenLog> GetElectrolyticTokenLogByID(long ID);
        Task<ElectrolyticTokenLog> AddElectrolyticTokenLog(ElectrolyticTokenLog objETokenLog);
        Task<ElectrolyticTokenLog> UpdateElectrolyticTokenLog(ElectrolyticTokenLog objETokenLog);
        bool DeleteElectrolyticTokenLog(long ID);
    }
}
