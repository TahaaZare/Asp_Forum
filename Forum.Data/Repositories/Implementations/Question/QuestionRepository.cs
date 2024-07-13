using Forum.Data.Context;
using Forum.Data.Repositories.Interfaces.Account;
using Forum.Data.Repositories.Interfaces.Question;
using Forum.Domain.Models.Account;
using Forum.Domain.ViewModels.Question;
using Microsoft.EntityFrameworkCore;
using Forum.Domain.Models.Questions;

namespace Forum.Data.Repositories.Implementations.Question;

public class QuestionRepository : IQuestionRepository
{
    #region ctor

    private ForumDbContext _context;

    private IUserRepository _userRepository;

    public QuestionRepository(ForumDbContext context, IUserRepository userRepository)
    {
        _context = context;
        _userRepository = userRepository;
    }

    #endregion

    #region userpanel

    public async Task<List<Domain.Models.Questions.Question>> UserQuestionList(string username)
    {
        var user = await _userRepository.GetUserByUserName(username);
        var questions = await _context.Questions
            .Where(q => q.User_id == user.Id)
            .Where(q=>q.Status)
            .Where(q=>q.Description == null)
            .ToListAsync();

        return questions;
    }

    #endregion

    #region question

    public async Task<Domain.Models.Questions.Question> GetQuestionById(long id)
    {
        return await _context.Questions.FirstOrDefaultAsync(q => q.Id == id);
    }

    public async Task<Domain.Models.Questions.Question> GetQuestionDetailById(long id)
    {
        return await _context.Questions
            .FirstOrDefaultAsync(q => q.Id == id);
    }

    public async Task<List<Domain.Models.Questions.Question>> QuestionList()
    {
        var questions = await _context.Questions
            .Where(q => q.Deleted_at == null)
            .Where(q => q.Status)
            .Include(c => c.QuestionCategory).Include(u => u.User)
            .ToListAsync();
        return questions;
    }

    public async Task<List<Domain.Models.Questions.Question>> QuestionListAdminPanel()
    {
        var questions = await _context.Questions
            .Include(c => c.QuestionCategory).Include(u => u.User)
            .ToListAsync();
        return questions;
    }

    public async Task ChangeVisibleQuestion(Domain.Models.Questions.Question question)
    {
        if (question.Status)
        {
            question.Status = false;
        }
        else
        {
            question.Status = true;
        }

        await UpdateQuestion(question);
        await SaveChanges();
    }

    public async Task<User> GetQuestionAuthor(Domain.Models.Questions.Question question)
    {
        var user = await _userRepository.GetUserByUserId(question.User_id);
        return user;
    }

    public async Task<bool> CheckVisitExisitForQuestion(string ip, long questionId)
    {
        return await _context.QuestionVisits.AnyAsync(s => s.User_Ip == ip && s.Question_id == questionId);
    }

    public async Task AddVisitForQuestion(QuestionVisit visit)
    {
        await _context.QuestionVisits.AddAsync(visit);
    }


    public async Task UpdateQuestion(Domain.Models.Questions.Question question)
    {
        _context.Update(question);
    }

    #endregion

    #region add question

    public async Task<List<Domain.Models.Questions.Question>> GetQuestionListByCategory(long categoryId)
    {
        return await _context.Questions.Where(s => s.Category_id == categoryId)
            .Where(q=>q.Status)
            .Where(q=>q.Deleted_at == null)
            .Include(c => c.QuestionCategory)
            .Include(u => u.User)
            .ToListAsync();
    }

    public async Task<bool> CheckQuestionTitle(string title)
    {
        var check_question_name = await _context.Questions.AnyAsync(q => q.Title == title);
        if (check_question_name == true)
        {
            return true;
        }

        return false;
    }

    public async Task AddQuestion(Domain.Models.Questions.Question question)
    {
        await _context.Questions.AddAsync(question);
    }

    #endregion

    #region add question tag

    public async Task AddQuestionTag(QuestionTag tag)
    {
        await _context.QuestionTags.AddAsync(tag);
    }

    #endregion

    #region question answer

    public async Task AddQuestionAnswer(Answer answer)
    {
        await _context.Answers.AddAsync(
            answer);
    }

    public async Task<List<Answer>> GetQuestionAnswer(Domain.Models.Questions.Question question)
    {
        var answer = await _context.Answers
            .Include(u => u.User)
            .Where(s => s.Question_id == question.Id)
            .OrderByDescending(s => s.Created_at)
            .ToListAsync();

        return answer;
    }


    public async Task<Answer> GetAnswerById(long id)
    {
        return await _context.Answers.FirstOrDefaultAsync(q => q.Id == id);
    }


    public async Task UpdateAnswer(Answer answer)
    {
        _context.Answers.Update(answer);
    }


    public async Task DeleteAnswer(Answer answer)
    {
        _context.Answers.Remove(answer);
    }

    public async Task<List<Domain.Models.Questions.Question>> GetFourLatesQuestion()
    {
        return await _context.Questions
            .Where(q=>q.Status)
            .Where(q=>q.Deleted_at == null)
            .Include(s => s.QuestionCategory)
            .Include(s => s.User).OrderByDescending(s => s.Created_at).Take(4).ToListAsync();
    }

    #endregion

    public async Task SaveChanges()
    {
        await _context.SaveChangesAsync();
    }
}