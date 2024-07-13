using Forum.Application.Services.Interfaces.Content.Banner;
using Microsoft.AspNetCore.Mvc;

namespace Forum.Web.ViewComponents;

public class BannerHomePageViewComponent : ViewComponent
{
    #region ctor

    private IBannerService _bannerService;

    public BannerHomePageViewComponent(IBannerService bannerService)
    {
        _bannerService = bannerService;
    }

    #endregion

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var banners = await _bannerService.GetBannerListFormAdminPanel();
        return View("BannerHomePage",banners);
    }
}