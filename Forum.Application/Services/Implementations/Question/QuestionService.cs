using Forum.Application.Security;
using Forum.Application.Services.Interfaces.Account;
using Forum.Application.Services.Interfaces.Question;
using Forum.Data.Repositories.Interfaces.Question;
using Forum.Domain.Models.Account;
using Forum.Domain.Models.Questions;
using Forum.Domain.ViewModels.Question;
using Forum.Domain.ViewModels.Question.Answer;
using Microsoft.EntityFrameworkCore;

namespace Forum.Application.Services.Implementations.Question;

public class QuestionService : IQuestionService
{
    #region ctor

    private IQuestionRepository _questionRepository;
    private IUserService _userService;
    private IQuestionTagService _questionTagService;

    public QuestionService(IQuestionRepository questionRepository, IUserService userService
        , IQuestionTagService questionTagService)
    {
        _questionRepository = questionRepository;
        _userService = userService;
        _questionTagService = questionTagService;
    }

    #endregion

    #region userpanel

    public async Task<List<Domain.Models.Questions.Question>> UserQuestionList(string username)
    {
        return await _questionRepository.UserQuestionList(username);
    }

    #endregion

    #region question

    public async Task<Domain.Models.Questions.Question> GetQuestionDetailById(long id)
    {
        return await _questionRepository.GetQuestionDetailById(id);
    }

    public async Task<User> GetQuestionAuthor(Domain.Models.Questions.Question question)
    {
        return await _questionRepository.GetQuestionAuthor(question);
    }

    public async Task<List<Domain.Models.Questions.Question>> QuestionList()
    {
        return await _questionRepository.QuestionList();
    }

    public async Task<List<Domain.Models.Questions.Question>> QuestionListAdminPanel()
    {
        return await _questionRepository.QuestionListAdminPanel();
    }

    public async Task ChangeVisibleQuestion(Domain.Models.Questions.Question question)
    {
        await _questionRepository.ChangeVisibleQuestion(question);
    }

    public async Task<CreateQuestionResult> CreateQuestion(CreateQuestionViewModel model)
    {
        var check_question_name = await _questionRepository.CheckQuestionTitle(model.Title);
        if (check_question_name == true)
        {
            return CreateQuestionResult.QuestionNameExist;
        }

        var question = new Domain.Models.Questions.Question()
        {
            Title = model.Title.SanitizeText(),
            Category_id = model.Category_id,
            User_id = model.User_id,
            Description = model.Description.SanitizeText(),
            Status = true,
        };
        await _questionRepository.AddQuestion(question);
        await _questionRepository.SaveChanges();

        foreach (var item in model.Tag_ids)
        {
            var question_tags = new QuestionTag()
            {
                Tag_id = item,
                Question_id = question.Id,
            };
            await _questionRepository.AddQuestionTag(question_tags);
            await _questionRepository.SaveChanges();
        }

        return CreateQuestionResult.Success;
    }


    public async Task<EditQuestionResult> EditQuestion(EditQuestionViewModel model)
    {
        var check_question_name = await _questionRepository.CheckQuestionTitle(model.Title);
        if (model.Title != model.Title)
        {
            if (check_question_name)
            {
                return EditQuestionResult.QuestionNameExist;
            }
        }

        var question = await GetQuestionById(model.Question_id);

        question.Title = model.Title.SanitizeText();
        question.Category_id = model.Category_id;
        question.Description = model.Description.SanitizeText();

        await _questionRepository.UpdateQuestion(question);
        await _questionRepository.SaveChanges();

        var tags = await _questionTagService.GetQuestionTag(question);
        foreach (var tagId in model.Tag_ids)
        {
            if (tags.All(t => t.Id != tagId))
            {
                var question_tags = new QuestionTag()
                {
                    Tag_id = tagId,
                    Question_id = question.Id,
                };
                await _questionRepository.AddQuestionTag(question_tags);
            }
        }

        await _questionRepository.SaveChanges();


        return EditQuestionResult.Success;
    }

    public async Task AddVisitForQuestion(string ip, Domain.Models.Questions.Question question)
    {
        if (await _questionRepository.CheckVisitExisitForQuestion(ip, question.Id))
        {
            return;
        }

        var visit = new QuestionVisit()
        {
            Question_id = question.Id,
            User_Ip = ip,
        };

        await _questionRepository.AddVisitForQuestion(visit);
        await _questionRepository.SaveChanges();

        question.Visit += 1;
        await _questionRepository.UpdateQuestion(question);
        await _questionRepository.SaveChanges();
    }

    public async Task<List<Domain.Models.Questions.Question>> GetQuestionListByCategory(long categoryId)
    {
        return await _questionRepository.GetQuestionListByCategory(categoryId);
    }

    public async Task<Domain.Models.Questions.Question> GetQuestionById(long id)
    {
        return await _questionRepository.GetQuestionById(id);
    }

    public async Task UpdateQuestion(Domain.Models.Questions.Question question)
    {
        await _questionRepository.UpdateQuestion(question);
    }

    public async Task<bool> CheckUserQuestion(string username, long questionId)
    {
        var user = await _userService.GetUserByUserName(username);
        var question = await _questionRepository.GetQuestionById(questionId);

        if (question.User_id != user.Id)
        {
            return false;
        }

        return true;
    }


    public async Task<EditQuestionViewModel> FillEditQuestionViewModel(string username, long questionId)
    {
        var user = await _userService.GetUserByUserName(username);
        var question = await _questionRepository.GetQuestionById(questionId);

        //if (question.User_id != user.Id && !user.Admin)
        //{
        //    return null;
        //}

        var result = new EditQuestionViewModel()
        {
            Question_id = question.Id,
            Title = question.Title,
            Category_id = question.Category_id,
            Description = question.Description,
        };

        return result;
    }

    #endregion

    #region question answer

    public async Task<bool> AnswerQuestion(AnswerQuestionViewModel model)
    {
        var question = await _questionRepository.GetQuestionById(model.Question_id);
        if (question == null)
        {
            return false;
        }

        var answer = new Answer()
        {
            Description = model.Message.SanitizeText(),
            User_id = model.User_id,
            Question_id = model.Question_id
        };

        await _questionRepository.AddQuestionAnswer(answer);
        await _questionRepository.SaveChanges();

        return true;
    }

    public async Task<List<Answer>> GetQuestionAnswer(Domain.Models.Questions.Question question)
    {
        return await _questionRepository.GetQuestionAnswer(question);
    }


    public async Task<bool> HasUserAccessToSelectCorrectAnswer(string username, long answerId)
    {
        var answer = await _questionRepository.GetAnswerById(answerId);
        var question = await _questionRepository.GetQuestionById(answer.Question_id);

        if (answer == null)
        {
            return false;
        }

        var user = await _userService.GetUserByUserName(username);
        if (user.Admin == true)
        {
            return true;
        }

        if (question.User_id != user.Id)
        {
            return false;
        }

        return true;
    }


    public async Task<Answer> GetAnswerById(long id)
    {
        return await _questionRepository.GetAnswerById(id);
    }


    public async Task SelectCorrectAnswer(string username, long answerId)
    {
        var answer = await _questionRepository.GetAnswerById(answerId);

        answer.IsCorrectAnswer = !answer.IsCorrectAnswer;
        await this.UpdateAnswer(answer);
        await this.SaveChanges();
    }

    public async Task UpdateAnswer(Answer answer)
    {
        await _questionRepository.UpdateAnswer(answer);
    }


    public async Task<bool> CheckDeleteAnswer(string username, Answer answer)
    {
        if (await HasUserAccessToSelectCorrectAnswer(username, answer.Id))
        {
            await _questionRepository.DeleteAnswer(answer);
            await this.SaveChanges();
            return true;
        }

        return false;
    }


    public async Task<EditAnswerViewModel?> FillEditAnswerViewModel(string username, long answerId)
    {
        var answer = await GetAnswerById(answerId);

        if (answer == null) return null;

        var user = await _userService.GetUserByUserName(username);

        if (user == null) return null;

        if (user.Admin != true && user.Id != answer.User_id)
        {
            return null;
        }

        return new EditAnswerViewModel()
        {
            Message = answer.Description,
            Answer_id = answer.Id,
            User_id = user.Id,
            Question_id = answer.Question_id,
        };
    }

    public async Task<bool> EditAnswer(EditAnswerViewModel model)
    {
        var answer = await GetAnswerById(model.Answer_id);

        if (answer == null) return false;

        var user = await _userService.GetUserByUserId(model.User_id);

        if (user == null) return false;

        if (user.Admin != true && user.Id != answer.User_id)
        {
            return false;
        }

        answer.Description = model.Message;
        await UpdateAnswer(answer);
        await SaveChanges();

        return true;
    }


    public async Task<bool> CheckUserAnswer(string username, long answerId)
    {
        var answer = await GetAnswerById(answerId);

        if (answer == null) return false;

        var user = await _userService.GetUserByUserName(username);

        if (user == null) return false;

        if (user.Admin != true && user.Id != answer.User_id)
        {
            return false;
        }

        return true;
    }

    public async Task<List<Domain.Models.Questions.Question>> GetFourLatesQuestion()
    {
        return await _questionRepository.GetFourLatesQuestion();
    }

    #endregion

   

    public async Task SaveChanges()
    {
        await _questionRepository.SaveChanges();
    }
}