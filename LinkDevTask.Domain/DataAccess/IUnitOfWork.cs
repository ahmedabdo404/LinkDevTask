using LinkDevTask.Domain.Models;
using LinkDevTask.Domain.Repositories;

namespace LinkDevTask.Domain.DataAccess
{
    public interface IUnitOfWork : IDisposable
    {
        IBaseRepository<Job> _jobRepo { get; }
        IBaseRepository<Category> _categoryRepo { get; }

        int Save();
        Task<int> SaveAsync();
    }
}
