using LinkDevTask.Application.Consts;
using LinkDevTask.Application.Servcices.Interfaces;
using LinkDevTask.Application.ViewModels.Job;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace LinkDevTask.WebApp.Controllers
{
    [Authorize(Roles = UserRoles.User)]
    public class UserJobController : BaseController
    {
        #region ctor
        private readonly IUserJobService _userJobService;
        private readonly IJobService _jobService;
        public UserJobController(IUserJobService userService, IJobService jobService)
        {
            _userJobService = userService;
            _jobService = jobService;
        }
        #endregion
        public async Task<IActionResult> ApplyJob(string jobId)
        {
            var job = _jobService.GetJob(jobId);
            if (job is null || User.Identity?.Name is null)
            {
                return RedirectToErrorPage(HttpStatusCode.NotFound);
            }
            var applyResult = await _userJobService.ApplyJob(User.Identity.Name, jobId);

            if(applyResult.Succeeded)
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }

            return RedirectToErrorPage(HttpStatusCode.NotFound);
        }

        public IActionResult GetAppliedJobs()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetAppliedJobs(PageVM pager)
        {
            if(User.Identity?.Name is null)
            {
                return RedirectToErrorPage(HttpStatusCode.NotFound);
            }

            var searchValue = Request.Form["search[value]"];
            var dataTablePaged = await _userJobService.GetJobsOfUserByPage(pager, searchValue, User.Identity.Name);

            return Json(dataTablePaged);
        }

        public async Task<IActionResult> WithdrawApplication(string jobId)
        {
            if (User.Identity?.Name is null)
            {
                return RedirectToErrorPage(HttpStatusCode.NotFound);
            }

            var applyResult = await _userJobService.WithdrawApplication(User.Identity.Name, jobId);

            if (applyResult.Succeeded)
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }

            return RedirectToErrorPage(HttpStatusCode.NotFound);
        }
    }
}
