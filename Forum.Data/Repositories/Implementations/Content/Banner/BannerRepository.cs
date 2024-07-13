using Forum.Data.Context;
using Forum.Data.Repositories.Interfaces.Content.Banner;
using Forum.Domain.ViewModels.Content.Banner;
using Microsoft.EntityFrameworkCore;

namespace Forum.Data.Repositories.Implementations.Content.Banner;

public class BannerRepository : IBannerRepository
{
    #region ctor

    private ForumDbContext _context;

    public BannerRepository(ForumDbContext context)
    {
        _context = context;
    }

    #endregion

    #region Admin panel

    public async Task<List<Domain.Models.Content.Banner>> GetBannerListFormAdminPanel()
    {
        return await _context.Banners.ToListAsync();
    }

    public async Task<Domain.Models.Content.Banner> GetBannerById(long id)
    {
        return await _context.Banners.FirstAsync(s => s.Id == id
        );
    }

    #endregion

    #region Common

    public async Task AddBanner(Domain.Models.Content.Banner banner)
    {
        await _context.AddAsync(banner);
    }

    public async Task UpdateBanner(Domain.Models.Content.Banner banner)
    {
        _context.Banners.Update(banner);
    }

    public async Task DeleteBanner(Domain.Models.Content.Banner banner)
    {
        _context.Banners.Remove(banner);
    }

    public async Task SaveChanges()
    {
        await _context.SaveChangesAsync();
    }

    #endregion
}