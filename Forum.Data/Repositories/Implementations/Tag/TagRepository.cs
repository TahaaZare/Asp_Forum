using Forum.Data.Context;
using Forum.Data.Repositories.Interfaces.Tag;
using Microsoft.EntityFrameworkCore;

namespace Forum.Data.Repositories.Implementations.Tag;

public class TagRepository : ITagRepository
{
    #region ctor

    private ForumDbContext _context;

    public TagRepository(ForumDbContext context)
    {
        _context = context;
    }

    #endregion


    #region admin panel

    public async Task<List<Domain.Models.Tags.Tag>> GetTagListAdminPanel()
    {
        return await _context.Tags.ToListAsync();
    }

    public async Task CreateTagFromAdminPanel(Domain.Models.Tags.Tag tag)
    {
        await _context.Tags.AddAsync(tag);
    }

    public async Task<Domain.Models.Tags.Tag> GetTagById(long tagId)
    {
        return await _context.Tags.FirstOrDefaultAsync(t => t.Id == tagId);
    }

    public async Task<Domain.Models.Tags.Tag> FillTagForEditAdminPanel(long tagId)
    {
        var tag = await GetTagById(tagId);

        return tag;
    }

    public async Task UpdateTagFromAdminPanel(Domain.Models.Tags.Tag tag)
    {
        _context.Update(tag);
    }

    public async Task DeleteTagFromAdminPanel(Domain.Models.Tags.Tag tag)
    {
        _context.Tags.Remove(tag);
    }

    #endregion
    
    
    public async Task SaveChanges()
    {
        await _context.SaveChangesAsync();
    }
}