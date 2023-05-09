using System.Threading.Tasks;
using System;

namespace BecaworkService.Respository
{
    public interface IUnitOfWork : IDisposable
    {
        Task SaveChanges();
    }
}
