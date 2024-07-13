using Forum.Application.Services.Interfaces.Account;
using Forum.Domain.Enums.Admin.Account;
using Forum.Web.ActionFilters;
using Microsoft.AspNetCore.Mvc;

namespace Forum.Web.Areas.AdminPanel.Controllers;

[PermissionChecker("Manage Users")]
public class UserController : AdminBaseController
{
    #region ctor

    private IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    #endregion

    #region List

    public async Task<IActionResult> Index()
    {
        var users = await _userService.GetUserListForAdminPanel();
        return View(users);
    }

    #endregion

    #region Delete Or Recovery

    public async Task<IActionResult> DeleteOrRecovery(long userId)
    {
        var user = await _userService.GetUserByUserId(userId);
        if (user == null)
        {
            TempData[ErrorMessage] = "کاربر مورد نظر یافت نشد !";
            return RedirectToAction("Index", "User", new { area = "AdminPanel" });
        }

        var result = await _userService.DeleteOrRecoveryUserAccountFromAdminPanel(user.Id);


        switch (result)
        {
            case DeleteOrRecoveryAccount.DeletedSuccess:
                TempData[SuccessMessage] = "حساب کاربری مورد نظر با موفقیت حذفـ شد !";
                return RedirectToAction("Index", "User", new { area = "AdminPanel" });
            case DeleteOrRecoveryAccount.RecoverySuccess:
                TempData[SuccessMessage] = "حساب کاربری مورد نظر با موفقیت بازگردانده شد !";
                return RedirectToAction("Index", "User", new { area = "AdminPanel" });
            case DeleteOrRecoveryAccount.UserNotFound:
                TempData[ErrorMessage] = "کاربر مورد نظر یافت نشد !";
                break;
            case DeleteOrRecoveryAccount.UserIsBan:
                TempData[WarningMessage] = "حساب کاربری یا بن شده است !";
                break;
        }

        return RedirectToAction("Index", "User", new { area = "AdminPanel" });
    }

    #endregion

    #region Ban Or DisBan

    [HttpPost("ban-or-disban")]
    public async Task<IActionResult> BanOrDisBan(long userId)
    {
        var user = await _userService.GetUserByUserId(userId);
        if (user == null)
        {
            TempData[ErrorMessage] = "کاربر مورد نظر یافت نشد !";
            return RedirectToAction("Index", "User", new { area = "AdminPanel" });
        }

        var result = await _userService.BanOrNotBanAccountFromAdminPanel(user.Id);


        switch (result)
        {
            case BanOrNotBanAccount.DisBan:
                TempData[SuccessMessage] = "حساب کاربری مورد نظر با موفقیت فعال شد !";
                return RedirectToAction("Index", "User", new { area = "AdminPanel" });
            case BanOrNotBanAccount.SuccessBan:
                TempData[SuccessMessage] = "حساب کاربری مورد نظر با موفقیت بن شد !";
                return RedirectToAction("Index", "User", new { area = "AdminPanel" });
            case BanOrNotBanAccount.UserNotFound:
                TempData[ErrorMessage] = "کاربر مورد نظر یافت نشد !";
                break;
            case BanOrNotBanAccount.UserDeletedAccount:
                TempData[WarningMessage] = "حساب کاربری حذف شده است !";
                break;
        }

        return RedirectToAction("Index", "User", new { area = "AdminPanel" });
    }

    #endregion
}