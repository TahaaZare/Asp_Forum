using Forum.Application.Services.Interfaces.Tag;
using Forum.Data.Repositories.Interfaces.Tag;

namespace Forum.Application.Services.Implementations.Tag;

public class TagService : ITagService
{
    #region ctor

    private ITagRepository _tagRepository;

    public TagService(ITagRepository tagRepository)
    {
        _tagRepository = tagRepository;
    }

    #endregion
    
    
    #region admin panel

    public async Task<List<Domain.Models.Tags.Tag>> GetTagListAdminPanel()
    {
        return await _tagRepository.GetTagListAdminPanel();
    }

    public async Task CreateTagFromAdminPanel(Domain.Models.Tags.Tag tag)
    {
        await _tagRepository.CreateTagFromAdminPanel(tag);
    }

    public async Task<Domain.Models.Tags.Tag> GetTagById(long tagId)
    {
        return await _tagRepository.GetTagById(tagId);
    }

    public async Task<Domain.Models.Tags.Tag> FillTagForEditAdminPanel(long tagId)
    {
        var tag = await GetTagById(tagId);

        return tag;
    }

    public async Task UpdateTagFromAdminPanel(Domain.Models.Tags.Tag tag)
    {
        await _tagRepository.UpdateTagFromAdminPanel(tag);
    }

    public async Task DeleteTagFromAdminPanel(Domain.Models.Tags.Tag tag)
    {
        await _tagRepository.DeleteTagFromAdminPanel(tag);
    }

    #endregion
    
    
    public async Task SaveChanges()
    {
        await _tagRepository.SaveChanges();
    }
}