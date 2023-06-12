using LinkDevTask.Domain.Models;
using System.Linq.Expressions;

namespace LinkDevTask.Domain.Repositories
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);
        void Delete(TEntity entity);
        void DeleteRange(IEnumerable<TEntity> entities);
        TEntity? Find(string id);
        IQueryable<TEntity> GetAll(Expression<Func<TEntity, object>>? include = default);
        IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> match);
        Task<(IQueryable<TEntity>, int)> GetByPage(int skip, int take, Expression<Func<TEntity, bool>>? match = default,
            Expression<Func<TEntity, object>>? include = default);
        long GetCount(Expression<Func<TEntity, bool>>? match = null);
        TEntity? GetOne(Expression<Func<TEntity, bool>> match, Expression<Func<TEntity, object>>? include = null);
        void Update(TEntity entity);
        void UpdateRange(IEnumerable<TEntity> entities);
    }
}