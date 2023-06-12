using Microsoft.AspNetCore.Mvc;
using LinkDevTask.Application.ViewModels.Job;
using LinkDevTask.Application.Servcices.Interfaces;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LinkDevTask.WebApp.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IJobService _jobService;
        public HomeController(IJobService jobService)
        {
            _jobService = jobService;
        }

        public IActionResult Index()
        {
            ViewBag.TotalPages = _jobService.GetJobsCount();
            return View();
        }

        [Route("PageNotFound")]
        [HttpGet]
        public IActionResult Error()
        {
            return View();
        }
    }
}