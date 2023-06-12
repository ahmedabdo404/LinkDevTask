using LinkDevTask.Domain.DataAccess;
using LinkDevTask.Domain.Models;
using LinkDevTask.Domain.Repositories;
using LinkDevTask.Infrastructure.Repositories;

namespace LinkDevTask.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _db;
        public IBaseRepository<Job> _jobRepo { get; private set; }
        public IBaseRepository<Category> _categoryRepo { get; private set; }
        public UnitOfWork(AppDbContext db)
        {
            _db = db;
            _jobRepo = new BaseRepository<Job>(db);
            _categoryRepo = new BaseRepository<Category>(db);
        }

        public int Save()
        {
            return _db.SaveChanges();
        }

        public async Task<int> SaveAsync()
        {
            return await _db.SaveChangesAsync();
        }

        public void Dispose()
        {
            _db.Dispose();
        }

    }
}
