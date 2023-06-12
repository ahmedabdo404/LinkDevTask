using Microsoft.AspNetCore.Mvc;
using System.Net;
using LinkDevTask.WebApp.Filters;

namespace LinkDevTask.WebApp.Controllers
{
    [ExceptionHandling]
    public class BaseController : Controller
    {
        public IActionResult RedirectToErrorPage(HttpStatusCode Code, string message = default)
        {
            ViewData["StatusCode"] = (int)Code;
            ViewData["CodeMessage"] = Code.ToString();
            ViewData["ExceptionMessage"] = message;

            return View("Error");
        }
    }
}
