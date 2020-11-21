using System;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace OnlineAssessmentApplication.Filters
{

    public sealed class LogCustomExceptionFilterAttribute : FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            if (!filterContext.ExceptionHandled)
            {
                var exceptionMessage = filterContext.Exception.Message;
                var stackTrace = filterContext.Exception.StackTrace;
                var controllerName = filterContext.RouteData.Values["controller"].ToString();
                var actionName = filterContext.RouteData.Values["action"].ToString();

                string Message = "Date :" + DateTime.Now.ToString() + ", Controller: " + controllerName + ", Action:" + actionName +
                                 "Error Message : " + exceptionMessage
                                + Environment.NewLine + "Stack Trace : " + stackTrace;
                //saving the data in a text file called Log.txt
                //You can also save this in a dabase   
                File.AppendAllText(HttpContext.Current.Server.MapPath("~/ErrorPage/ErrorloggedFile.txt"), Message);

                filterContext.ExceptionHandled = true;
                filterContext.Result = new RedirectResult("~/ErrorPage/Error.html");
            }
        }
    }

}