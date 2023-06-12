using LinkDevTask.Application.ViewModels.Job;
using Microsoft.AspNetCore.Http;

namespace LinkDevTask.Application.Servcices.Interfaces
{
    public interface IJobService
    {
        Task<int> AddJob(JobVM newJob, IFormFile file);
        Task<int> DeleteJob(string id);
        Task<int> EditJob(JobVM newJob, IFormFile file);
        IEnumerable<JobVM> FilterJobsByName(string jobName);
        IEnumerable<JobVM> GetAll();
        JobVM? GetJob(string id);
        IEnumerable<JobVM> GetJobsByPage(PagedJobVM page, string searchValue);
        int GetJobsCount();
    }
}
