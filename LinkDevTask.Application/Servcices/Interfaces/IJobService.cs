using LinkDevTask.Application.ViewModels.Job;
using Microsoft.AspNetCore.Http;

namespace LinkDevTask.Application.Servcices.Interfaces
{
    public interface IJobService
    {
        Task<int> AddJob(JobVM newJob, IFormFile file);
        Task<int> DeleteJob(string id);
        Task<int> EditJob(JobVM newJob, IFormFile file);
        IEnumerable<JobVM> GetAll();
        JobVM? GetJob(string id);
        Task<PagedJobVM> GetJobsByPage(PageVM page, string searchValue);
        int GetJobsCount();
    }
}
