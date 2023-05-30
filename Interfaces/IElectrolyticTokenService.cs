using BecaworkService.Models;
using BecaworkService.Models.Responses;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BecaworkService.Interfaces
{
    public interface IElectrolyticTokenService
    {
        Task<QueryResult<ElectrolyticToken>> GetElectrolyticTokens([FromQuery] QueryParams queryParams);
        Task<ElectrolyticToken> GetElectrolyticTokenByID(long ID);
        Task<ElectrolyticToken> AddElectrolyticToken(ElectrolyticToken objElectrolyticToken);
        Task<ElectrolyticToken> UpdateElectrolyticToken(ElectrolyticToken objEToken);
        bool DeteleElectrolyticToken(long ID);
    }
}
