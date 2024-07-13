using Forum.Application.Services.Interfaces.Question;
using Forum.Web.ActionFilters;
using Microsoft.AspNetCore.Mvc;

namespace Forum.Web.Areas.AdminPanel.Controllers;

[PermissionChecker("Manage Questions")]
public class QuestionController : AdminBaseController
{
    #region ctor

    private IQuestionService _questionService;

    public QuestionController(IQuestionService questionService)
    {
        _questionService = questionService;
    }

    #endregion

    #region list

    public async Task<IActionResult> Index()
    {
        var questions = await _questionService.QuestionListAdminPanel();
        return View(questions);
    }

    #endregion

    #region change visible

    [HttpPost("change-question-visible")]
    public async Task<IActionResult> ChangeVisible(long id)
    {
        var question = await _questionService.GetQuestionById(id);
        await _questionService.ChangeVisibleQuestion(question);
        TempData[SuccessMessage] = "عملیات با موفقیت انجام شد !";
        return RedirectToAction("Index", "Question", new { area = "AdminPanel" });
    }

    #endregion
}