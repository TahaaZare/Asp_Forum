using Microsoft.AspNetCore.Mvc;

namespace Forum.Web.Controllers;

public class BaseController : Controller
{
    public static string SuccessMessage = "SuccessMessage";
    public static string InfoMessage = "InfoMessage";
    public static string WarningMessage = "WarningMessage";
    public static string ErrorMessage = "ErrorMessage";
}