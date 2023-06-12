using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Net;

namespace LinkDevTask.WebApp.Filters
{
    public class ExceptionHandlingAttribute : ExceptionFilterAttribute
    {
        public override Task OnExceptionAsync(ExceptionContext context)
        {
            if (context.ExceptionHandled)
            {
                return Task.CompletedTask;
            }
            context.ExceptionHandled = true;
            var resultView = new ViewResult()
            {
                ViewData = new ViewDataDictionary(new EmptyModelMetadataProvider(), context.ModelState) {
                    { "StatusCode", (int)HttpStatusCode.InternalServerError},
                    { "CodeMessage", HttpStatusCode.InternalServerError.ToString()},
                    { "ExceptionMessage", context.Exception.Message}
                },
                ViewName = "Error"
            };
            context.Result = resultView;
            return Task.CompletedTask;
        }
    }
}
