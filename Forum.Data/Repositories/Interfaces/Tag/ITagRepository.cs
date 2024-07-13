namespace Forum.Data.Repositories.Interfaces.Tag;

public interface ITagRepository
{
    #region Admin panel

    Task<List<Domain.Models.Tags.Tag>> GetTagListAdminPanel();

    Task CreateTagFromAdminPanel(Domain.Models.Tags.Tag tag);
    Task<Domain.Models.Tags.Tag> GetTagById(long tagId);
    Task<Domain.Models.Tags.Tag> FillTagForEditAdminPanel(long tagId);
    Task UpdateTagFromAdminPanel(Domain.Models.Tags.Tag tag);
    Task DeleteTagFromAdminPanel(Domain.Models.Tags.Tag tag);

    #endregion

    Task SaveChanges();
}