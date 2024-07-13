using Forum.Domain.ViewModels.Content.Banner;
using Microsoft.AspNetCore.Http;

namespace Forum.Application.Services.Interfaces.Content.Banner;

public interface IBannerService
{
    #region Admin Panel
    Task<List<Domain.Models.Content.Banner>> GetBannerListFormAdminPanel();
    Task<CreateBannerResult> CreateBannerFromAdminPanel(CreateBannerViewModel model);
    Task<EditBannerViewModel> FillEditBannerViewModel(long id);
    Task<EditBannerResult> EditBannerFromAdminPanel(EditBannerViewModel model);
    Task<bool> DeleteBanner(Domain.Models.Content.Banner banner);
    Task<Domain.Models.Content.Banner> GetBannerById(long id);


    #endregion
}