using Forum.Domain.Models.Account;
using Forum.Domain.Models.Questions;
using Forum.Domain.ViewModels.Question;
using Forum.Domain.ViewModels.Question.Answer;

namespace Forum.Application.Services.Interfaces.Question;

public interface IQuestionService
{
    #region userpanel

    Task<List<Domain.Models.Questions.Question>> UserQuestionList(string username);

    #endregion

    #region question

    Task<Domain.Models.Questions.Question> GetQuestionDetailById(long id);
    Task<User> GetQuestionAuthor(Domain.Models.Questions.Question question);
    Task<List<Domain.Models.Questions.Question>> QuestionList();
    Task<CreateQuestionResult> CreateQuestion(CreateQuestionViewModel model);
    Task<EditQuestionResult> EditQuestion(EditQuestionViewModel model);
    Task AddVisitForQuestion(string ip, Domain.Models.Questions.Question question);
    Task UpdateQuestion(Domain.Models.Questions.Question question);
    Task<List<Forum.Domain.Models.Questions.Question>> GetQuestionListByCategory(long categoryId);

    #endregion

    #region admin panel

    Task<List<Domain.Models.Questions.Question>> QuestionListAdminPanel();

    Task ChangeVisibleQuestion(Domain.Models.Questions.Question question);

    #endregion

    #region question answer

    Task<Domain.Models.Questions.Question> GetQuestionById(long id);
    Task<bool> AnswerQuestion(AnswerQuestionViewModel model);
    Task<List<Answer>> GetQuestionAnswer(Domain.Models.Questions.Question question);
    Task<bool> HasUserAccessToSelectCorrectAnswer(string username, long answerId);
    Task<Answer> GetAnswerById(long id);
    Task SelectCorrectAnswer(string username, long answerId);
    Task UpdateAnswer(Answer answer);
    Task<bool> CheckUserQuestion(string username, long questionId);
    Task<EditQuestionViewModel> FillEditQuestionViewModel(string username, long questionId);
    Task<bool> CheckDeleteAnswer(string username, Answer answer);
    Task<bool> CheckUserAnswer(string username, long answerId);
    Task<EditAnswerViewModel?> FillEditAnswerViewModel(string username, long answerId);
    Task<bool> EditAnswer(EditAnswerViewModel model);
    Task<List<Domain.Models.Questions.Question>> GetFourLatesQuestion();

    #endregion


    Task SaveChanges();
}