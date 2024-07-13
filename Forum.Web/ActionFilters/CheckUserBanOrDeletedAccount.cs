using Forum.Application.Extensions;
using Forum.Application.Services.Interfaces.Account;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Authentication;

namespace Forum.Web.ActionFilters
{
    public class CheckUserBanOrDeletedAccount : Attribute, IAsyncAuthorizationFilter
    {
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var userService = (IUserService)context.HttpContext.RequestServices.GetService(typeof(IUserService))!;
            var userId = context.HttpContext.User.GetUserId();
            if (await userService.CheckUserBanOrDeletedAccount(userId) == true)
            {
                await context.HttpContext.SignOutAsync();
                context.Result = new ForbidResult();
            }
        }
    }
}