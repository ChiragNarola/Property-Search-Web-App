using System.Linq.Expressions;

namespace PropertySearch.Data.Repository.Interface
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> Query();
        IQueryable<T> Query(Expression<Func<T, bool>> filter,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            IList<Expression<Func<T, object>>>? includes = null,
            int? page = null,
            int? pageSize = null);


        T? GetOne(
            Expression<Func<T, bool>> filter,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            IList<Expression<Func<T, object>>>? includes = null,
            int? page = null,
            int? pageSize = null);
        Task<T?> GetOneAsync(
            Expression<Func<T, bool>> filter,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            IList<Expression<Func<T, object>>>? includes = null,
            int? page = null,
            int? pageSize = null);


        Task<List<T>> GetAllAsync();
        List<T> Get(
            Expression<Func<T, bool>> filter,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            IList<Expression<Func<T, object>>>? includes = null,
            int? page = null,
            int? pageSize = null);
        Task<List<T>> GetAsync(
            Expression<Func<T, bool>> filter,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            IList<Expression<Func<T, object>>>? includes = null,
            int? page = null,
            int? pageSize = null);


        T? GetById(int id);
        Task<T?> GetByIdAsync(int id);
        T? GetById(long id);
        Task<T?> GetByIdAsync(long id);
        T? GetById(Guid id);
        T? GetById(params object[] keyValues);
        Task<T?> GetByIdAsync(params object[] keyValues);
        Task<T?> GetByIdAsync(Guid id);


        T Add(T entity);
        Task<T> AddAsync(T entity);
        void AddRange(IEnumerable<T> entities);
        Task AddRangeAsync(IEnumerable<T> entities);


        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
        void Remove(Expression<Func<T, bool>> filter);
        void RemoveRange(Expression<Func<T, bool>> filter);

        void Update(T entity);
        Task<T> UpdateAsync(T entity);

        bool Any(Expression<Func<T, bool>> predicate);
        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);


        long LongCount(Expression<Func<T, bool>> predicate);
        Task<long> LongCountAsync(Expression<Func<T, bool>> predicate);
        long LongCount();
        Task<long> LongCountAsync();


        List<T> GetView();
    }
}
