using System.Web.Mvc;

namespace OnlineAssessmentApplication.Filters
{
    public sealed class StudentAuthorizeFilterAttribute : FilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (!filterContext.RequestContext.HttpContext.User.IsInRole("Student"))
            {
                filterContext.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary(new { Controller = "dashboard", Action = "index" }));
            }
        }
    }
}