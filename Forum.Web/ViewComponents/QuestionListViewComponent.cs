using Forum.Application.Services.Interfaces.Question;
using Microsoft.AspNetCore.Mvc;

namespace Forum.Web.ViewComponents;

public class QuestionListViewComponent : ViewComponent
{
    #region ctor

    private IQuestionService _questionService;

    public QuestionListViewComponent(IQuestionService questionService)
    {
        _questionService = questionService;
    }

    #endregion

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var questions = await _questionService.QuestionList();
        
        return View("QuestionList",questions);
    }
}