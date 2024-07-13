using Forum.Application.Services.Interfaces.Question;
using Forum.Data.Repositories.Interfaces.Question;
using Forum.Domain.Models.Tags;

namespace Forum.Application.Services.Implementations.Question;

public class QuestionTagService : IQuestionTagService
{
    #region ctor

    private IQuestionTagRepository _questionTagRepository;

    public QuestionTagService(IQuestionTagRepository questionTagRepository)
    {
        _questionTagRepository = questionTagRepository;
    }

    #endregion


    public async Task<List<Domain.Models.Tags.Tag>> GetAllTags()
    {
        return await _questionTagRepository.GetAllTags();
    }

    public async Task<IQueryable<Domain.Models.Tags.Tag>> GetAllTagsAsQueryable()
    {
        return await _questionTagRepository.GetAllTagsAsQueryable();
    }

    public async Task<List<Domain.Models.Tags.Tag>> GetQuestionTag(Domain.Models.Questions.Question question)
    {
        return await _questionTagRepository.GetQuestionTag(question);
    }
}