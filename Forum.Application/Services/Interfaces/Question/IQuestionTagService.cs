using Forum.Domain.Models.Questions;
using Forum.Domain.Models.Tags;

namespace Forum.Application.Services.Interfaces.Question;

public interface IQuestionTagService
{
    Task<List<Domain.Models.Tags.Tag>> GetAllTags();
    Task<IQueryable<Domain.Models.Tags.Tag>> GetAllTagsAsQueryable();
    Task<List<Domain.Models.Tags.Tag>> GetQuestionTag(Domain.Models.Questions.Question question);


}