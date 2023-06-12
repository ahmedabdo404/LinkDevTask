using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDevTask.Application.Servcices.Interfaces
{
    public interface IUserJobService
    {
        Task<IdentityResult> ApplyJob(string userName, string JobId);
        Task<int> GetNumberOfAppliedUsers(string userName, string JobId);
        Task<bool> IsUserApplied(string userName, string JobId);
        Task<IdentityResult> WithdrawApplication(string userName, string JobId);
    }
}
