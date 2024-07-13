using Forum.Application.Extensions;
using Forum.Application.Services.Interfaces.Account;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Forum.Web.ActionFilters;

public class PermissionChecker : Attribute, IAsyncAuthorizationFilter
{
    #region ctor

    private readonly string _permissionName;

    public PermissionChecker(string permissionName)
    {
        _permissionName = permissionName;
    }

    #endregion

    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        var userService = (IUserService)context.HttpContext.RequestServices.GetService(typeof(IUserService))!;
        if (!await userService.CheckUserPermission(_permissionName.ToLower(), context.HttpContext.User.GetUserId()))
        {
            context.Result = new ForbidResult();
        }
    }
}