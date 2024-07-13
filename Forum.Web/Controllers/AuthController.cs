using System.Security.Claims;
using Forum.Application.Services.Interfaces.Account;
using Forum.Domain.ViewModels.Auth;
using Forum.Web.ActionFilters;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace Forum.Web.Controllers;

public class AuthController : BaseController
{
    #region ctor

    private IUserService _userService;

    public AuthController(IUserService userService)
    {
        _userService = userService;
    }

    #endregion

    #region Register

    [HttpGet("register")]
    [RedirectHomeIfLogin]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost("register")]
    [RedirectHomeIfLogin]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var result = await _userService.RegisterUser(model);
        switch (result)
        {
            case RegisterResult.Success:
                TempData[SuccessMessage] = "عملیات با موفقیت انجام شـد !";
                return RedirectToAction("Index", "Home");
            case RegisterResult.UserNameExist:
                TempData[WarningMessage] = "نام کاربری معتبر نمیباشـد !";
                break;
            default:
                TempData[ErrorMessage] = "خطا غیر منتظـره !";
                break;
        }

        return View(model);
    }

    #endregion

    #region Login

    [HttpGet("login")]
    [RedirectHomeIfLogin]
    public IActionResult Login(string ReturnUrl = "")
    {
        var result = new LoginViewModel();
        if (!string.IsNullOrEmpty(ReturnUrl))
        {
            result.ReturnUrl = ReturnUrl;
        }
        return View(result);
    }

    [HttpPost("login")]
    [RedirectHomeIfLogin]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var result = await _userService.CheckUserForLogin(model);
        switch (result)
        {
            case LoginResult.Success:
                var user = await _userService.GetUserByUserName(model.Username);

                #region auth

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                    new Claim(ClaimTypes.Name,user.Username),
                };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);
                var properties = new AuthenticationProperties { IsPersistent = true };

                await HttpContext.SignInAsync(principal, properties);

                #endregion
                
                TempData[SuccessMessage] = $"کاربر {model.Username} خوش آمدید :)";
                if (!string.IsNullOrEmpty(model.ReturnUrl))
                {
                    return Redirect(model.ReturnUrl);
                }
                return RedirectToAction("Index", "Home");
            case LoginResult.Ban:
                TempData[WarningMessage] = "حساب کاربری شمـا مسدود میباشـد !";
                break;
            case LoginResult.NotFound:
                TempData[ErrorMessage] = "حساب کاربری مورد نظر یافت نشـد !";
                break;
        }

        return View(model);
    }

    #endregion

    #region Logout

    [HttpGet("logout")]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync();
        return RedirectToAction("Login", "Auth");
    }

    #endregion
}