using Forum.Domain.Models.Questions;
using Forum.Domain.Models.Tags;

namespace Forum.Data.Repositories.Interfaces.Question;

public interface IQuestionTagRepository
{
    Task<List<Domain.Models.Tags.Tag>> GetAllTags();
    Task<IQueryable<Domain.Models.Tags.Tag>> GetAllTagsAsQueryable();
    Task<List<Domain.Models.Tags.Tag>> GetQuestionTag(Domain.Models.Questions.Question question);
}