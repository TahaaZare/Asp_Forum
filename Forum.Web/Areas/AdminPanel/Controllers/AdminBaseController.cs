﻿using Forum.Web.ActionFilters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Forum.Web.Areas.AdminPanel.Controllers;

[Area("AdminPanel")]
[Authorize]
[PermissionChecker("Admin Panel")]
public class AdminBaseController : Controller
{
    public static string SuccessMessage = "SuccessMessage";
    public static string InfoMessage = "InfoMessage";
    public static string WarningMessage = "WarningMessage";
    public static string ErrorMessage = "ErrorMessage";
}