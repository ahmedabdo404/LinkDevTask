using LinkDevTask.Application.Servcices.Interfaces;
using LinkDevTask.Application.ViewModels.Job;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace LinkDevTask.WebApp.Controllers
{
    public class JobController : BaseController
    {
        private readonly IJobService _jobService;
        private readonly ICategoryService _categoryService;
        public JobController(IJobService jobService, ICategoryService categoryService)
        {
            _jobService = jobService;
            _categoryService = categoryService;
        }

        public IActionResult GetAll()
        {
            var allJobs = _jobService.GetAll();

            return View(allJobs);
        }

        [HttpPost]
        public IActionResult GetPagedJobs(PagedJobVM pager)
        {
            var searchValue = Request.Form["search[value]"];
            var pagedJobs = _jobService.GetJobsByPage(pager, searchValue);
            var totalPages = _jobService.GetJobsCount();

            return Json(new { recordsFiltered = totalPages, data = pagedJobs });
        }

        public IActionResult GetbyPage(string jobName)
        {
            var jobs = _jobService.FilterJobsByName(jobName);
            return View(jobs);
        }

        public IActionResult Add()
        {
            ViewBag.categories = _categoryService.GetAll();
            return View(new JobVM());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(JobVM job)
        {
            if (ModelState.IsValid)
            {
                await _jobService.AddJob(job, Request.Form.Files?.FirstOrDefault());
                return RedirectToAction(nameof(GetAll));
            }
            ViewBag.categories = _categoryService.GetAll();
            return View(job);
        }

        public IActionResult Edit(string id)
        {
            var job = _jobService.GetJob(id);
            if(job is null)
            {
                return RedirectToErrorPage(HttpStatusCode.NotFound);
            }
            ViewBag.categories = _categoryService.GetAll();
            return View(job);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(JobVM job)
        {
            if (ModelState.IsValid)
            {
                await _jobService.EditJob(job, Request.Form.Files?.FirstOrDefault());
                return RedirectToAction(nameof(GetAll));
            }
            ViewBag.categories = _categoryService.GetAll();
            return View(job);
        }

        public async Task<IActionResult> Delete(string id)
        {
            await _jobService.DeleteJob(id);
            return RedirectToAction(nameof(GetAll));
        }

        public IActionResult ViewJob(string id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction(nameof(AccountController.Login), "Account");
            }

            var job = _jobService.GetJob(id);
            if (job is null)
            {
                return RedirectToErrorPage(HttpStatusCode.NotFound);
            }
            return View(job);
        }
    }
}
