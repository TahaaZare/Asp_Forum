using System.Diagnostics;
using Forum.Application.Extensions;
using Forum.Application.Services.Interfaces.Question;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Forum.Web.Controllers;

public class HomeController : BaseController
{
    #region ctor

    private IQuestionService _questionService;
    private IQuestionCategoryService _questionCategoryService;

    public HomeController(IQuestionService questionService, IQuestionCategoryService questionCategoryService)
    {
        _questionService = questionService;
        _questionCategoryService = questionCategoryService;
    }

    #endregion
    public IActionResult Index()
    {
        return View();
    }

    #region Questio list by catgory

    [HttpGet("questions/c/{categoryName}")]
    public async Task<IActionResult> QuestionListByCategory(string categoryName)
    {
        var category = await _questionCategoryService.GetQuestionCategoryByName(categoryName);
        if (category == null)
        {
            TempData[InfoMessage] = "دسته بندی مورد نظر یافت نشد !";
            return RedirectToAction("QuestionList", "Home");
        }

        var questions = await _questionService.GetQuestionListByCategory(category.Id);
        return View(questions);
    }
    

    #endregion
    
    #region QuestionList

    [HttpGet("questions")]
    public async Task<IActionResult> QuestionList()
    {
        return View();
    }

    #endregion

    #region Question detail

    [HttpGet("questions/{id}")]
    public async Task<IActionResult> QuestionDetail(long id)
    {
        var question = await _questionService.GetQuestionDetailById(id);
        if (question.Deleted_at != null)
        {
            TempData[InfoMessage] = "سوال مورد نظـر یافت نشـد !";
            return RedirectToAction("QuestionList", "Home");
        }else if (question.Status == false)
        {
            TempData[InfoMessage] = "سوال مورد نظـر یافت نشـد !";
            return RedirectToAction("QuestionList", "Home");
        }
        
        var user_ip = Request.HttpContext.Connection.RemoteIpAddress;

        await _questionService.AddVisitForQuestion(user_ip.ToString(), question);

        return View(question);
    }

    [HttpGet("q/{id}")]
    public async Task<IActionResult> QuestionDetailByShortLink(long id)
    {
        var question = await _questionService.GetQuestionById(id);
        if (question == null)
        {
            TempData[WarningMessage] = "سوال مورد نظر یافت نشـد !";
            return RedirectToAction("QuestionList", "Home");
        }

        return RedirectToAction("QuestionDetail", "Home", new { id = id });
    }

    #endregion

    #region Select correct Answer

    [HttpPost("correct-answer/{answerId}")]
    [Authorize]
    public async Task<IActionResult> SelectCorrectAnswer(long answerId)
    {
        var answer = await _questionService.GetAnswerById(answerId);

        if (!await _questionService.HasUserAccessToSelectCorrectAnswer(HttpContext.User.GetUserName(), answerId))
        {
            TempData[WarningMessage] = "دسترسی شما محدود شده است !!!";
            return RedirectToAction("QuestionDetail", "Home", new { id = answer.Question_id });
        }

        await _questionService.SelectCorrectAnswer(HttpContext.User.GetUserName(), answerId);
        TempData[SuccessMessage] = "عملیات با موفقیت انجام شـد .";
        return RedirectToAction("QuestionDetail", "Home", new { id = answer.Question_id });
    }


    #endregion
}