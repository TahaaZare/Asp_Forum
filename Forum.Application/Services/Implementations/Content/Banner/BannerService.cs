using Forum.Application.Extensions;
using Forum.Application.Services.Interfaces.Content.Banner;
using Forum.Application.Statics;
using Forum.Data.Context;
using Forum.Data.Repositories.Interfaces.Content.Banner;
using Forum.Domain.ViewModels.Content.Banner;
using Microsoft.AspNetCore.Http;

namespace Forum.Application.Services.Implementations.Content.Banner;

public class BannerService : IBannerService
{
    #region ctor

    private IBannerRepository _bannerRepository;

    public BannerService(IBannerRepository bannerRepository)
    {
        _bannerRepository = bannerRepository;
    }

    #endregion

    public async Task<List<Domain.Models.Content.Banner>> GetBannerListFormAdminPanel()
    {
        return await _bannerRepository.GetBannerListFormAdminPanel();
    }

    public async Task<CreateBannerResult> CreateBannerFromAdminPanel(CreateBannerViewModel model)
    {
        var fileName = Guid.NewGuid() + Path.GetExtension(model.Image.FileName);
        var uploadImage = model.Image.UploadFile(fileName, Paths.BannerServerPath);
        if (uploadImage == false)
        {
            return CreateBannerResult.ImageUploadFailed;
        }

        var banner = new Domain.Models.Content.Banner()
        {
            Title = model.Title,
            Url = model.Url,
            Image = fileName
        };
        await _bannerRepository.AddBanner(banner);
        await _bannerRepository.SaveChanges();

        return CreateBannerResult.Success;
    }

    public async Task<EditBannerViewModel> FillEditBannerViewModel(long id)
    {
        var banner = await _bannerRepository.GetBannerById(id);
        var result = new EditBannerViewModel()
        {
            Id = banner.Id,
            Title = banner.Title,
            Url = banner.Url,
            ImageFileName = banner.Image
        };

        return result;
    }


    public async Task<EditBannerResult> EditBannerFromAdminPanel(EditBannerViewModel model)
    {
        var banner = await _bannerRepository.GetBannerById(model.Id);
        if (banner == null)
        {
            return EditBannerResult.NotFound;
        }

        if (banner.Image == null)
        {
            var avatarDeleted = FileExtensions.DeleteCurrentBannerIMage(banner);
        
            var fileName = Guid.NewGuid() + Path.GetExtension(model.Image.FileName);
            var result = model.Image.UploadFile(fileName, Paths.BannerServerPath);
            
            banner.Image = fileName;
        }
        else if ( model.Image != null)
        {
            if (banner.Image != model.Image.FileName )
            {
                var avatarDeleted = FileExtensions.DeleteCurrentBannerIMage(banner);
        
                var fileName = Guid.NewGuid() + Path.GetExtension(model.Image.FileName);
                var result = model.Image.UploadFile(fileName, Paths.BannerServerPath);
            
                banner.Image = fileName;
            }
           
        }
        else if(model.Image == null)
        {
            banner.Image = model.ImageFileName;
        }

        banner.Title = model.Title;
        banner.Url = model.Url;

        await _bannerRepository.UpdateBanner(banner);
        await _bannerRepository.SaveChanges();

        return EditBannerResult.Success;
    }

    public async Task<bool> DeleteBanner(Domain.Models.Content.Banner banner)
    {
        var avatarDeleted = FileExtensions.DeleteCurrentBannerIMage(banner);
        await _bannerRepository.DeleteBanner(banner);
        await _bannerRepository.SaveChanges();

        return true;
    }

    public async Task<Domain.Models.Content.Banner> GetBannerById(long id)
    {
        return await _bannerRepository.GetBannerById(id);
    }
}