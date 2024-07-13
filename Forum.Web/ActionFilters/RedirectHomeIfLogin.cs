using Microsoft.AspNetCore.Mvc.Filters;

namespace Forum.Web.ActionFilters;

public class RedirectHomeIfLogin : ActionFilterAttribute
{
    public override void OnActionExecuted(ActionExecutedContext context)
    {
        base.OnActionExecuted(context);
        if (context.HttpContext.User.Identity.IsAuthenticated)
        {
            context.HttpContext.Response.Redirect("/");
        }
    }
}