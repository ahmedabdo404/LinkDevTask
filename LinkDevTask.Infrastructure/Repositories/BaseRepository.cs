using LinkDevTask.Domain.Models;
using LinkDevTask.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq.Expressions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;


namespace LinkDevTask.Infrastructure.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        private readonly AppDbContext _db;
        public BaseRepository(AppDbContext db)
        {
            _db = db;
        }

        public IQueryable<TEntity> GetAll(Expression<Func<TEntity, object>>? include = default)
        {
            IQueryable<TEntity> AllData = _db.Set<TEntity>();
            if (include is not null)
            {
                AllData = AllData.Include(include);
            }
            return AllData;
        }
        public IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> match)
        {
            IQueryable<TEntity> AllData = _db.Set<TEntity>();
            return AllData.Where(match);
        }

        public TEntity? Find(string id)
        {
            return _db.Set<TEntity>().Find(id);
        }

        public TEntity? GetOne(Expression<Func<TEntity, bool>> match, Expression<Func<TEntity, object>>? include = default)
        {
            IQueryable<TEntity> query = _db.Set<TEntity>();
            if (include is not null)
            {
                query = query.Include(include);
            }
            return query.FirstOrDefault(match);
        }

        public void Add(TEntity entity)
        {
            _db.Add(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            _db.AddRange(entities);
        }

        public void Update(TEntity entity)
        {
            _db.Update(entity);
        }

        public void UpdateRange(IEnumerable<TEntity> entities)
        {
            _db.UpdateRange(entities);
        }

        public void Delete(TEntity entity)
        {
            _db.Remove(entity);
        }

        public void DeleteRange(IEnumerable<TEntity> entities)
        {
            _db.RemoveRange(entities);
        }

        public int GetCount(Expression<Func<TEntity, bool>>? match = null)
        {
            var query = _db.Set<TEntity>();
            return match is null ? query.Count() : query.Count(match);
        }

        public async Task<(IQueryable<TEntity>, int)> GetByPage(int skip, int take, Expression<Func<TEntity, bool>>? match = default,
            Expression<Func<TEntity, object>>? include = default)
        {
            IQueryable<TEntity> query = _db.Set<TEntity>();

            if (include is not null)
            {
                query = query.Include(include);
            }
            if (match is not null)
            {
                query = query.Where(match);
            }
            var count = await query.CountAsync();
            return (query.Skip(skip).Take(take), count);
        }
    }
}
