using PropertySearch.Data.Models;
using PropertySearch.Data.Repository.Interface;
using Microsoft.EntityFrameworkCore.Storage;

namespace PropertySearch.Data
{
    public interface IUnitOfWork
    {
        IRepository<T> Repository<T>() where T : class;

        int Save();
        Task<int> SaveAsync();

        IDbContextTransaction BeginTransaction();
        void CommitTransaction();
        void RollbackTransaction();

        Task<IDbContextTransaction> BeginTransactionAsync();
        public Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
    }

}
