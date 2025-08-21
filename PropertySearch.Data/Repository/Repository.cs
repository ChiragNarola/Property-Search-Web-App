using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using PropertySearch.Data.DBContext;
using PropertySearch.Data.Repository.Interface;

namespace PropertySearch.Data.Repository
{
    public class Repository<T>(PropertySearchDBContext context) : IRepository<T> where T : class
    {
        protected readonly PropertySearchDBContext _context = context;

        #region Query
        public IQueryable<T> Query()
            => _context.Set<T>().AsQueryable();
        public IQueryable<T> Query(
            Expression<Func<T, bool>> filter,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            IList<Expression<Func<T, object>>>? includes = null,
            int? page = null,
            int? pageSize = null)
        {
            var query = _context.Set<T>().AsQueryable();
            if (includes != null)
                query = includes.Aggregate(query, (current, include) => current.Include(include));
            if (filter != null)
                query = query.Where(filter);
            if (orderBy != null)
                query = orderBy(query);
            if (page != null && pageSize != null)
                query = query.Skip((page.Value - 1) * pageSize.Value).Take(pageSize.Value);
            return query;
        }
        #endregion

        #region Get List
        public async Task<List<T>> GetAllAsync() => await _context.Set<T>().ToListAsync();
        public List<T> Get(
            Expression<Func<T, bool>>? filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            IList<Expression<Func<T, object>>>? includes = null,
            int? page = null,
            int? pageSize = null)
        {
            var query = _context.Set<T>().AsQueryable();

            if (includes != null)
                query = includes.Aggregate(query, (current, include) => current.Include(include));
            if (orderBy != null)
                query = orderBy(query);
            if (filter != null)
                query = query.Where(filter);
            if (page != null && pageSize != null)
                query = query.Skip((page.Value - 1) * pageSize.Value).Take(pageSize.Value);
            return [.. query];
        }
        public async Task<List<T>> GetAsync(
            Expression<Func<T, bool>>? filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            IList<Expression<Func<T, object>>>? includes = null,
            int? page = null,
            int? pageSize = null)
        {
            var query = _context.Set<T>().AsQueryable();

            if (includes != null)
                query = includes.Aggregate(query, (current, include) => current.Include(include));
            if (orderBy != null)
                query = orderBy(query);
            if (filter != null)
                query = query.Where(filter);
            if (page != null && pageSize != null)
                query = query.Skip((page.Value - 1) * pageSize.Value).Take(pageSize.Value);
            return await query.ToListAsync();
        }
        #endregion

        #region Get One
        public T? GetOne(
            Expression<Func<T, bool>> filter,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            IList<Expression<Func<T, object>>>? includes = null,
            int? page = null,
            int? pageSize = null)
            => Query(filter, orderBy, includes, page, pageSize).FirstOrDefault();
        public Task<T?> GetOneAsync(
            Expression<Func<T, bool>> filter,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            IList<Expression<Func<T, object>>>? includes = null,
            int? page = null,
            int? pageSize = null)
            => Query(filter, orderBy, includes, page, pageSize).FirstOrDefaultAsync();
        #endregion

        #region Get By Id
        public T? GetById(int id) => _context.Set<T>().Find(id);
        public T? GetById(long id) => _context.Set<T>().Find(id);
        public T? GetById(Guid id) => _context.Set<T>().Find(id);
        public T? GetById(params object[] keyValues) => _context.Set<T>().Find(keyValues);
        public async Task<T?> GetByIdAsync(long id) => await _context.Set<T>().FindAsync(id);
        public async Task<T?> GetByIdAsync(int id) => await _context.Set<T>().FindAsync(id);
        public async Task<T?> GetByIdAsync(params object[] keyValues) => await _context.Set<T>().FindAsync(keyValues);
        public async Task<T?> GetByIdAsync(Guid id) => await _context.Set<T>().FindAsync(id);
        #endregion

        #region Add
        public T Add(T entity)
        {
            _context.Set<T>().Add(entity);
            return entity;
        }
        public async Task<T> AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            return entity;
        }
        public void AddRange(IEnumerable<T> entities) => _context.Set<T>().AddRange(entities);
        public async Task AddRangeAsync(IEnumerable<T> entities) => await _context.Set<T>().AddRangeAsync(entities);
        #endregion

        #region Any
        public bool Any(Expression<Func<T, bool>> predicate) => _context.Set<T>().Where(predicate).Any();
        public Task<bool> AnyAsync(Expression<Func<T, bool>> predicate) => _context.Set<T>().Where(predicate).AnyAsync();
        #endregion

        #region Long count
        public long LongCount(Expression<Func<T, bool>> predicate) => _context.Set<T>().Where(predicate).LongCount();
        public Task<long> LongCountAsync(Expression<Func<T, bool>> predicate) => _context.Set<T>().Where(predicate).LongCountAsync();
        public long LongCount() => _context.Set<T>().LongCount();
        public Task<long> LongCountAsync() => _context.Set<T>().LongCountAsync();
        #endregion

        #region Remove
        public void Remove(T entity) => _context.Set<T>().Remove(entity);
        public void RemoveRange(IEnumerable<T> entities) => _context.Set<T>().RemoveRange(entities);
        public void Remove(Expression<Func<T, bool>> filter)
        {
            var ent = GetOne(filter);
            if (ent != null)
                _context.Set<T>().Remove(ent);
        }
        public void RemoveRange(Expression<Func<T, bool>> filter)
        {
            var ent = Get(filter);
            if (ent != null && ent.Any())
                _context.Set<T>().RemoveRange(ent);
        }
        #endregion

        #region Update
        public void Update(T entity) => _context.Entry(entity).State = EntityState.Modified;
        public async Task<T> UpdateAsync(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;  
            await _context.SaveChangesAsync();  
            return entity;
        }
        #endregion

        public List<T> GetView() => [.. _context.Set<T>()];
    }
}