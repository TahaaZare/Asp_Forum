using Forum.Application.Services.Interfaces.Content.Banner;
using Forum.Domain.ViewModels.Content.Banner;
using Forum.Web.ActionFilters;
using Microsoft.AspNetCore.Mvc;

namespace Forum.Web.Areas.AdminPanel.Controllers;

[PermissionChecker("Manage Banners")]
public class BannerController : AdminBaseController
{
    #region ctor

    private IBannerService _bannerService;

    public BannerController(IBannerService bannerService)
    {
        _bannerService = bannerService;
    }

    #endregion

    #region banner list

    [HttpGet("banner-list")]
    public async Task<IActionResult> Index()
    {
        var banners = await _bannerService.GetBannerListFormAdminPanel();

        return View(banners);
    }

    #endregion

    #region create

    [HttpGet("create-banner")]
    public async Task<IActionResult> Create()
    {
        return View();
    }

    [HttpPost("create-banner"), ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateBannerViewModel model)
    {
        if (!ModelState.IsValid)
        {
            TempData[ErrorMessage] = "خطایی رخ داده است !";
            return View(model);
        }

        var result = await _bannerService.CreateBannerFromAdminPanel(model);
        switch (result)
        {
            case CreateBannerResult.Success:
                TempData[SuccessMessage] = "عملیات با موفقیت انجام شد";
                return RedirectToAction("Index", "Banner", new { area = "AdminPanel" });
            case CreateBannerResult.ImageUploadFailed:
                TempData[WarningMessage] = "اپلود عکس با خطا مواجه شد ! دوباره تلاش کنید !";
                break;
        }

        return View(model);
    }

    #endregion

    #region Edit

    [HttpGet("edit-banner/{id}")]
    public async Task<IActionResult> Edit(long id)
    {
        var result = await _bannerService.FillEditBannerViewModel(id);
        if (result == null)
        {
            return NotFound();
        }

        return View(result);
    }

    [HttpPost("edit-banner/{id}"), ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(EditBannerViewModel model)
    {
        if (!ModelState.IsValid)
        {
            TempData[ErrorMessage] = "خطایی رخ داده است !";
            return View(model);
        }

        var result = await _bannerService.EditBannerFromAdminPanel(model);
        switch (result)
        {
            case EditBannerResult.Success:
                TempData[SuccessMessage] = "عملیات با موفقیت انجام شد";
                return RedirectToAction("Index", "Banner", new { area = "AdminPanel" });
            case EditBannerResult.NotFound:
                TempData[ErrorMessage] = " #1خطایی رخ داده است !";
                break;
        }


        return View(model);
    }

    #endregion

    #region delete

    [HttpPost("delete-banner/{id}")]
    public async Task<IActionResult> Delete(long id)
    {
        var banner = await _bannerService.GetBannerById(id);
        if (banner == null)
        {
            return NotFound();
        }

        var result = await _bannerService.DeleteBanner(banner);

        if (result)
        {
            TempData[SuccessMessage] = "عملیات با موفقیت انجام شد";
            return RedirectToAction("Index", "Banner", new { area = "AdminPanel" });
        }

        TempData[ErrorMessage] = "خطایی رخ داده است !";
        return RedirectToAction("Index", "Banner", new { area = "AdminPanel" });
    }

    #endregion
}