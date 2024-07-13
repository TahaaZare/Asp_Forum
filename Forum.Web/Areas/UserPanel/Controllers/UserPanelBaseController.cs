using Forum.Web.ActionFilters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Forum.Web.Areas.UserPanel.Controllers;

[Area("UserPanel")]
[Authorize]
[CheckUserBanOrDeletedAccount]
public class UserPanelBaseController : Controller
{
    public static string SuccessMessage = "SuccessMessage";
    public static string InfoMessage = "InfoMessage";
    public static string WarningMessage = "WarningMessage";
    public static string ErrorMessage = "ErrorMessage";
}