using Forum.Application.Extensions;
using Forum.Application.Services.Interfaces.Account;
using Forum.Application.Statics;
using Forum.Web.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Forum.Web.Areas.UserPanel.Controllers;

public class HomeController : UserPanelBaseController
{
    #region ctor

    private IUserService _userService;

    public HomeController(IUserService userService)
    {
        _userService = userService;
    }

    #endregion

    public IActionResult Index()
    {
        TempData["Title"] = "پنل کاربری";
        return View();
    }

    #region Change Avatar

    public async Task<IActionResult> ChangeUserAvatar(IFormFile UserAvatar)
    {
        var user = await _userService.GetUserByUserName(HttpContext.User.GetUserName());
        var avatarDeleted = FileExtensions.DeleteCurrentAvatar(user);
        
        var fileName = Guid.NewGuid() + Path.GetExtension(UserAvatar.FileName);
        var result = UserAvatar.UploadFile(fileName, Paths.UserAvatarServerPath);

        await _userService.ChangeUserAvatar(HttpContext.User.GetUserName(), fileName);

        return new JsonResult(new
        {
            status = "Success"
        });
    }

    #endregion
}