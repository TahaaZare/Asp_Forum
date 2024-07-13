using Forum.Application.Extensions;
using Forum.Application.Services.Interfaces.Account;
using Forum.Domain.ViewModels.Account.UserPanel;
using Microsoft.AspNetCore.Mvc;

namespace Forum.Web.Areas.UserPanel.Controllers;

public class AccountController : UserPanelBaseController
{
    #region ctor

    private IUserService _userService;

    public AccountController(IUserService userService)
    {
        _userService = userService;
    }

    #endregion


    #region edit user

    [HttpGet("edit-profile")]
    public async Task<IActionResult> EditProfile()
    {
        TempData["Title"] = "ویرایش حساب کاربری";

        var result = await _userService.FillEditUserViewModel(HttpContext.User.GetUserName());
        return View(result);
    }

    [HttpPost("edit-profile"), ValidateAntiForgeryToken]
    public async Task<IActionResult> EditProfile(EditUserViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var result = await _userService.EditUserProfile(model);
        switch (result)
        {
            case EditUserResult.Success:
                TempData[SuccessMessage] = "عملیات ویرایش با موفقیت انجام شـد .";
                return RedirectToAction("Index", "Home", new { area = "UserPanel" });
            case EditUserResult.UserNameExists:
                TempData[WarningMessage] = "نام کاربری جدید قبلا انتخاب شده است !";
                break;
            case EditUserResult.UserNotFound:
                TempData[ErrorMessage] = "کاربری یافت نشـد !";
                break;
        }

        return View(model);
    }

    #endregion
}