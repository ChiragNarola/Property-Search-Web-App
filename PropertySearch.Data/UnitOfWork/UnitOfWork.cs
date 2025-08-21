using PropertySearch.Data.DBContext;
using PropertySearch.Data.Repository.Interface;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;

namespace PropertySearch.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PropertySearchDBContext _context;
        private readonly IServiceProvider _serviceProvider;

        public UnitOfWork(
            PropertySearchDBContext context,
            IServiceProvider serviceProvider
            )
        {
            _serviceProvider = serviceProvider;
            _context = context;
        }

        public IRepository<T> Repository<T>() where T : class => _serviceProvider.GetRequiredService<IRepository<T>>();

        public int Save() => _context.Save();
        public Task<int> SaveAsync() => _context.SaveAsync();

        public IDbContextTransaction BeginTransaction() => _context.Database.BeginTransaction();
        public void CommitTransaction() => _context.Database.CommitTransaction();
        public void RollbackTransaction() => _context.Database.RollbackTransaction();

       // public Task<IDbContextTransaction> BeginTransactionAsync() => _context.Database.BeginTransactionAsync();
        public Task CommitTransactionAsync() => _context.Database.CommitTransactionAsync();
        public Task RollbackTransactionAsync() => _context.Database.RollbackTransactionAsync();

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            var transaction = await _context.Database.BeginTransactionAsync();
            return transaction;
        }
    }
}
