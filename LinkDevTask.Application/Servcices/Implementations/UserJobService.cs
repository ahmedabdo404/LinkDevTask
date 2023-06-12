using LinkDevTask.Application.Servcices.Interfaces;
using LinkDevTask.Domain.DataAccess;
using LinkDevTask.Domain.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace LinkDevTask.Application.Servcices.Implementations
{
    public class UserJobService : IUserJobService
    {
        #region ctor

        private readonly IUnitOfWork _unitOfWork;
        readonly UserManager<User> _userManager;
        public UserJobService(IUnitOfWork unitOfWork, UserManager<User> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        #endregion

        public async Task<IdentityResult> ApplyJob(string userName, string JobId)
        {
            var user = await _userManager.FindByNameAsync(userName);
            var job = _unitOfWork._jobRepo.Find(JobId);
            if (user is not null && job is not null)
            {
                user.UserJob.Add(new()
                {
                    UserId = user.Id,
                    JobId = job.Id,
                });
                return await _userManager.UpdateAsync(user);
            }

            return IdentityResult.Failed();
        }

        public async Task<IdentityResult> WithdrawApplication(string userName, string JobId)
        {
            var user = await _userManager.FindByNameAsync(userName);
            var job = _unitOfWork._jobRepo.GetOne(j => j.Id.Equals(JobId), uj => uj.UserJob);
            if (user is not null && job is not null)
            {
                job.UserJob.Remove(job.UserJob.First(uj => uj.UserId.Equals(user.Id) && uj.JobId.Equals(job.Id)));
                return await _userManager.UpdateAsync(user);
            }

            return IdentityResult.Failed();
        }

        public async Task<bool> IsUserApplied(string userName, string JobId)
        {
            var user = await _userManager.FindByNameAsync(userName);
            var job = _unitOfWork._jobRepo.GetOne(j => j.Id.Equals(JobId), uj => uj.UserJob);
            if (user is not null && job is not null)
            {
                return job.UserJob.Any(uj => uj.UserId.Equals(user.Id));
            }
            return default;
        }

        public async Task<int> GetNumberOfAppliedUsers(string userName, string JobId)
        {
            var user = await _userManager.FindByNameAsync(userName);
            var job = _unitOfWork._jobRepo.GetOne(j => j.Id.Equals(JobId), uj => uj.UserJob);
            if (user is not null && job is not null)
            {
                return job.UserJob.Count;
            }
            return default;
        }
    }
}
