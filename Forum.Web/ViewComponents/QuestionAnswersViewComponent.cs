using Forum.Application.Services.Interfaces.Question;
using Forum.Domain.Models.Questions;
using Forum.Domain.ViewModels.Question;
using Forum.Domain.ViewModels.Question.Answer;
using Microsoft.AspNetCore.Mvc;

namespace Forum.Web.ViewComponents;

public class QuestionAnswersViewComponent : ViewComponent
{
    #region ctor

    private IQuestionService _questionService;

    public QuestionAnswersViewComponent(IQuestionService questionService)
    {
        _questionService = questionService;
    }

    #endregion

    public async Task<IViewComponentResult> InvokeAsync(Question question)
    {
        var answers = await _questionService.GetQuestionAnswer(question);
        var viewModel = new QuestionAnswersViewModel
        {
            Question = question,
            Answers = answers
        };
        return View("QuestionAnswers", viewModel);
    }
}