using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Threading.Tasks;
using System;
using BecaworkService.Models;

namespace BecaworkService.Respository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BecaworkDbContext _context;
        private readonly string _libraryConnectionString;
        private readonly ILogger _logger;

        #region Library DBSet Repositories
        private IAsyncRepository<Notification> _notificationRepository;
        private IAsyncRepository<Mail> _mailRepository;
        private IAsyncRepository<FCMTokenLog> _fCMTokenLogRepository;
        private IAsyncRepository<FCMToken> _fCMTokenRepository;
        private IAsyncRepository<ElectrolyticToken> _electrolyticTokenRepository;
        #endregion

        #region LibraryContext Repository
        public IAsyncRepository<Notification> NotificationRepository => _notificationRepository ??= new EfRepository<Notification>(_context);
        public IAsyncRepository<Mail> MailRepository => _mailRepository ??= new EfRepository<Mail>(_context);
        public IAsyncRepository<FCMTokenLog> FCMTokenLogRepository => _fCMTokenLogRepository ??= new EfRepository<FCMTokenLog>(_context);
        public IAsyncRepository<FCMToken> FCMTokenRepository => _fCMTokenRepository ??= new EfRepository<FCMToken>(_context);
        public IAsyncRepository<ElectrolyticToken> ElectrolyticTokenRepository => _electrolyticTokenRepository ??= new EfRepository<ElectrolyticToken>(_context);

        #endregion

        public UnitOfWork(string libraryConnectionString)
        {
            _libraryConnectionString = libraryConnectionString;
            var libraryOptionsBuilder = new DbContextOptionsBuilder<BecaworkDbContext>();
            libraryOptionsBuilder.UseSqlServer(_libraryConnectionString);
            _context = new BecaworkDbContext(libraryOptionsBuilder.Options);
        }

        private static List<T> ConvertDataTable<T>(DataTable dt)
        {
            List<T> data = new();
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            return data;
        }

        private static T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    if (pro.Name == column.ColumnName)
                        pro.SetValue(obj, dr.IsNull(column.ColumnName) ? null : dr[column.ColumnName], null);
                    else
                        continue;
                }
            }
            return obj;
        }

        public async Task SaveChanges()
        {
            const string loggerHeader = "UnitOfWork";
            using var dbContextTransaction = await _context.Database.BeginTransactionAsync();
            try
            {
                await _context.SaveChangesAsync();
                await dbContextTransaction.CommitAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{loggerHeader} have error: {ex.Message}, and inner exception is: {ex.InnerException?.ToString()}");
                await dbContextTransaction.RollbackAsync();
                throw;
            }
        }

        private bool _disposedValue = false;

        protected virtual async Task Dispose(bool disposing)
        {
            if (_disposedValue) return;

            if (disposing)
            {
                await _context.DisposeAsync();
            }

            _disposedValue = true;
        }

        public void Dispose()
        {
            Dispose(true).Wait();
            GC.SuppressFinalize(this);
        }
    }
}
