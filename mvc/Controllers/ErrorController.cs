using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace testing1.Controllers
{
    //THIRD PARTY LOGGING PROVIDER :
                 //nuget install
        // NLog : NLog.Web.AspNetCore 
    public class ErrorController :Controller
    {
        private readonly ILogger<ErrorController> logger;

        // INJECT UNTUK LOGGING ERROR 
        public ErrorController(ILogger<ErrorController> logger)
        {
            this.logger = logger;
        }

        [Route("Error/{statusCode}")]
        public IActionResult HttpStatusCodeHanlder(int statusCode)
        {
            var statusCodeResult = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();
            switch (statusCode)
            {
                case 404:
                    logger.LogWarning($"404 Error Occured. Path = {statusCodeResult.OriginalPath} and Query String = {statusCodeResult.OriginalQueryString}");

                    ViewBag.ErrorMessage = "Sorry, the resource cant be found";
                    ViewBag.Path = statusCodeResult.OriginalPath;
                    ViewBag.QS = statusCodeResult.OriginalQueryString;
                    break;
            }
            return View("NotFound");
        }
        [Route("Error")]
        [AllowAnonymous]
        public IActionResult Error()
        {
            var exceptionDetails = HttpContext.Features.Get<IExceptionHandlerPathFeature>();

            // UNTUK LOG DI CONSOLE :
            logger.LogError($"The path {exceptionDetails.Path} threw an exception {exceptionDetails.Error}");

            //UNTUK LOG DI CLIENT SIDE :
                ViewBag.ExceptionPath = exceptionDetails.Path;
                ViewBag.ExceptionMessage = exceptionDetails.Error.Message;
                ViewBag.StackTrace = exceptionDetails.Error.StackTrace;

            //TODO :
                //CREATE DATABASE UNTUK ERROR TIAP KALI ADA ERROR MASUK KE SINI INSERT DATA ERROR
            return View("Error");
        }
    }
}
