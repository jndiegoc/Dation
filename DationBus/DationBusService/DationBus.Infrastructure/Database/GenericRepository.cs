using CommonUtils.Models;
using CommonUtils.Wrappers;
using DationBus.Business.Database;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace DationBus.Infrastructure.Database
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class, new()
    {
        public readonly ApplicationDbContext _context;
        public GenericRepository(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }
        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public void AddRange(IEnumerable<T> entities)
        {
            _context.Set<T>().AddRange(entities);
        }

        public IQueryable<T> GetAll()
        {
            return _context.Set<T>();
        }

        public T? GetById(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public void Remove(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            _context.Set<T>().RemoveRange(entities);
        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }

        public void UpdateMany(Expression<Func<T, bool>> filterExpression, Action<T> setProperty)
        {
            // Get the records to be updated depending on the filter expression
            var recordsToBeUpdated = _context.Set<T>().Where(filterExpression).ToList();

            // Update the selected records
            recordsToBeUpdated.ForEach(setProperty);
        }

        public PagedData<T> GetPerPage(PaginationFilter filter)
        {
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
            var pagedData = _context.Set<T>()
                .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                .Take(validFilter.PageSize)
                .ToList();
            var totalRecords = _context.Set<T>().Count();

            return new PagedData<T>
            {
                TotalRecords = totalRecords,
                Data = pagedData
            };
        }
        public PagedData<T> GetPerPageMyQuery(PaginationFilter filter, IQueryable<T> query)
        {
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
            var pagedData = query
                .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                .Take(validFilter.PageSize)
                .ToList();
            var totalRecords = query.Count();

            return new PagedData<T>
            {
                TotalRecords = totalRecords,
                Data = pagedData
            };
        }
        
        public IQueryable<T> GetFromQuery(string spQuery, params object[] parameters)
        {
            var dbSetT = _context.Set<T>();
            return dbSetT.FromSql(FormattableStringFactory.Create(spQuery, parameters));
        }

        public IEnumerable<T2> GetFromQuery<T2>(string spQuery, params object[] parameters)
        {
            return _context.Database.SqlQuery<T2>(FormattableStringFactory.Create(spQuery, parameters));
        }

        public async Task<int> ExecuteDeleteAsync(Expression<Func<T, bool>> predicate)
        {
            var entitiesToDelete = _context.Set<T>().Where(predicate);
            return await entitiesToDelete.ExecuteDeleteAsync();
        }

    }
}
