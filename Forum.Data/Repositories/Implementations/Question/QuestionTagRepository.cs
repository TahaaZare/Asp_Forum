using Forum.Data.Context;
using Forum.Data.Repositories.Interfaces.Question;
using Forum.Domain.Models.Questions;
using Forum.Domain.Models.Tags;
using Microsoft.EntityFrameworkCore;

namespace Forum.Data.Repositories.Implementations.Question;

public class QuestionTagRepository : IQuestionTagRepository
{
    #region ctor

    private ForumDbContext _context;

    public QuestionTagRepository(ForumDbContext context)
    {
        _context = context;
    }

    #endregion

    public async Task<List<Domain.Models.Tags.Tag>> GetAllTags()
    {
        return await _context.Tags.ToListAsync();
    }

    public async Task<IQueryable<Domain.Models.Tags.Tag>> GetAllTagsAsQueryable()
    {
        return _context.Tags.AsQueryable();
    }

    public async Task<List<Domain.Models.Tags.Tag>> GetQuestionTag(Domain.Models.Questions.Question question)
    {
        var questionTags = await _context.QuestionTags.Where(t => t.Question_id == question.Id).ToListAsync();
        List<Domain.Models.Tags.Tag> tags = new List<Domain.Models.Tags.Tag>();

        foreach (var item in questionTags)
        {
            var tag = await _context.Tags.FirstOrDefaultAsync(t =>
                t.Id == item.Tag_id);
            if (tag != null)
            {
                tags.Add(tag);
            }
        }

        return tags;
    }
}