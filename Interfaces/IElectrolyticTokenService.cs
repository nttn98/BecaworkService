using BecaworkService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BecaworkService.Interfaces
{
    public interface IElectrolyticTokenService
    {
        Task<IEnumerable<ElectrolyticToken>> GetElectrolyticTokens(int page, int pageSize);
        Task<ElectrolyticToken> GetElectrolyticTokenByID(long ID);
        Task<ElectrolyticToken> AddElectrolyticToken(ElectrolyticToken objElectrolyticToken);
        Task<ElectrolyticToken> UpdateElectrolyticToken(ElectrolyticToken objElectrolyticToken);
        bool DeteleElectrolyticToken(long ID);
    }
}
