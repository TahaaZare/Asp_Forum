using Forum.Domain.ViewModels.Content.Banner;

namespace Forum.Data.Repositories.Interfaces.Content.Banner;

public interface IBannerRepository
{
    #region Admin Panel

    Task<List<Domain.Models.Content.Banner>> GetBannerListFormAdminPanel();


    #endregion

    #region Common

    Task<Domain.Models.Content.Banner> GetBannerById(long id);
    Task AddBanner(Domain.Models.Content.Banner banner);
    Task UpdateBanner(Domain.Models.Content.Banner banner);
    Task DeleteBanner(Domain.Models.Content.Banner banner);
    Task SaveChanges();

    #endregion
}