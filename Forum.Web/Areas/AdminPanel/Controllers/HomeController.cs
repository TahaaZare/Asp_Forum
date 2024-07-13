using Forum.Application.Services.Interfaces.Question;
using Microsoft.AspNetCore.Mvc;

namespace Forum.Web.Areas.AdminPanel.Controllers;

public class HomeController : AdminBaseController
{
    #region ctor

    #endregion

    [HttpGet("admin-panel")]
    public IActionResult AdminDashboard()
    {
        return View();
    }
}