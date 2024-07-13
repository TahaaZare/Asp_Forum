using Forum.Domain.Models.Account;
using Forum.Domain.Models.Questions;
using Forum.Domain.ViewModels.Question;

namespace Forum.Data.Repositories.Interfaces.Question;

public interface IQuestionRepository
{
    #region userpanel

    Task<List<Domain.Models.Questions.Question>> UserQuestionList(string username);

    #endregion

    #region question

    Task<Domain.Models.Questions.Question> GetQuestionById(long id);
    Task<Domain.Models.Questions.Question> GetQuestionDetailById(long id);
    Task<List<Domain.Models.Questions.Question>> QuestionList();
    Task<User> GetQuestionAuthor(Domain.Models.Questions.Question question);
    Task<bool> CheckVisitExisitForQuestion(string ip, long questionId);
    Task AddVisitForQuestion(QuestionVisit visit);
    Task UpdateQuestion(Domain.Models.Questions.Question question);
    Task<List<Domain.Models.Questions.Question>> GetFourLatesQuestion();
    Task<List<Domain.Models.Questions.Question>> GetQuestionListByCategory(long categoryId);

    #endregion

    #region admin panel
    Task<List<Domain.Models.Questions.Question>> QuestionListAdminPanel();
    Task ChangeVisibleQuestion(Domain.Models.Questions.Question question);
    #endregion

    #region add question

    Task<bool> CheckQuestionTitle(string title);
    Task AddQuestion(Domain.Models.Questions.Question question);

    #endregion

    #region add question tag

    Task AddQuestionTag(QuestionTag tag);

    #endregion

    #region question answer

    Task AddQuestionAnswer(Answer answer);
    Task<List<Answer>> GetQuestionAnswer(Domain.Models.Questions.Question question);
    Task<Answer> GetAnswerById(long id);
    Task UpdateAnswer(Answer answer);
    Task DeleteAnswer(Answer answer);

    #endregion


    Task SaveChanges();
}