using Forum.Application.Extensions;
using Forum.Application.Security;
using Forum.Application.Services.Interfaces.Account;
using Microsoft.AspNetCore.Mvc;

namespace Forum.Web.Areas.UserPanel.ViewComponents;

public class UserInfoViewComponent : ViewComponent
{
    #region ctor

    private IUserService _userService;

    public UserInfoViewComponent(IUserService userService)
    {
        _userService = userService;
    }

    #endregion

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var user = await _userService.GetUserByUserName(HttpContext.User.GetUserName());

        if (user.Avatar == null)
        {
            #region gravavatar

            string hash = HashMD5.MD5Hash(user.Username);
            string gravatarUrl = $"https://www.gravatar.com/avatar/{hash}?d=identicon";
            user.Avatar = gravatarUrl;

            #endregion

            await _userService.UpdateUser(user);
            await _userService.SaveChangeAsync();
        }
        
        return View("UserInfo",user);
    }
}