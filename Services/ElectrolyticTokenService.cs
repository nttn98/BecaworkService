using BecaworkService.Interfaces;
using BecaworkService.Models;
using BecaworkService.Respository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BecaworkService.Services
{
    public class ElectrolyticTokenService : IElectrolyticTokenService
    {
        private readonly BecaworkDbContext _context;

        public ElectrolyticTokenService(BecaworkDbContext context)
        {
            _context = context;
        }
        public async Task<ElectrolyticToken> AddElectrolyticToken(ElectrolyticToken objElectrolyticToken)
        {
            _context.ElectrolyticTokens.Add(objElectrolyticToken);
            await _context.SaveChangesAsync();
            return objElectrolyticToken;
        }

        public bool DeteleElectrolyticToken(long ID)
        {
            bool result = false;
            var tempEToken = _context.ElectrolyticTokens.Find(ID);
            if (tempEToken != null)
            {
                _context.Entry(tempEToken).State = EntityState.Deleted;
                _context.SaveChanges();
                result = true;
            }
            else
            {
                result = false;
            }
            return result;
        }

        public async Task<ElectrolyticToken> GetElectrolyticTokenByID(long ID)
        {
            var tempEToken = await _context.ElectrolyticTokens.FindAsync(ID);
            return tempEToken;
        }

        public async Task<IEnumerable<ElectrolyticToken>> GetElectrolyticTokens(int page, int pageSize)
        {
            if (page == 0 && pageSize == 0 || pageSize == 0)
            {
                var ETokens = await _context.ElectrolyticTokens.ToListAsync();
                return ETokens;
            }
            else if (page == 0)
            {
                var ETokens = _context.ElectrolyticTokens.ToList().Take(pageSize);
                return ETokens;
            }
            else
            {
                var ETokens = _context.ElectrolyticTokens.ToList().Skip((page - 1) * pageSize).Take(pageSize);
                return ETokens;
            }
        }

        public async Task<ElectrolyticToken> UpdateElectrolyticToken(ElectrolyticToken objElectrolyticToken)
        {
            _context.Entry(objElectrolyticToken).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return objElectrolyticToken;
        }
    }
}
