using System.Linq.Expressions;
using CommonUtils.Wrappers;
using CommonUtils.Models;

namespace DationBus.Business.Database
{
    public interface IGenericRepository<T> where T : class, new()
    {
        T? GetById(int id);
        IQueryable<T> GetAll();
        PagedData<T> GetPerPage(PaginationFilter filter);
        PagedData<T> GetPerPageMyQuery(PaginationFilter filter, IQueryable<T> query);
        void Add(T entity);
        void Update(T entity);
        void UpdateMany(Expression<Func<T, bool>> filterExpression, Action<T> setProperty);
        void AddRange(IEnumerable<T> entities);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
        Task<int> ExecuteDeleteAsync(Expression<Func<T, bool>> predicate);
        IQueryable<T> GetFromQuery(string spQuery, params object[] parameters);
        IEnumerable<T2> GetFromQuery<T2>(string spQuery, params object[] parameters);
    }
}
