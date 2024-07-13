using Forum.Application.Extensions;
using Forum.Application.Security;
using Forum.Application.Services.Interfaces.Account;
using Forum.Application.Services.Interfaces.Question;
using Forum.Data.Repositories.Interfaces.Question;
using Forum.Domain.Models.Questions;
using Forum.Domain.ViewModels.Question;
using Forum.Domain.ViewModels.Question.Answer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Forum.Web.Areas.UserPanel.Controllers;

public class QuestionController : UserPanelBaseController
{
    #region ctor

    private IQuestionCategoryService _questionCategorService;
    private IQuestionTagService _questionTagService;
    private IUserService _userService;

    private IQuestionService _questionService;

    public QuestionController(IQuestionCategoryService questionCategorService, IQuestionTagService questionTagService,
        IUserService userService, IQuestionService questionService)
    {
        _questionCategorService = questionCategorService;
        _questionTagService = questionTagService;
        _userService = userService;
        _questionService = questionService;
    }

    #endregion

    #region create question

    [Authorize]
    [HttpGet("create-question")]
    public async Task<IActionResult> CreateQuestion()
    {
        return View();
    }

    [Authorize]
    [HttpPost("create-question"), ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateQuestion(CreateQuestionViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var user = await _userService.GetUserByUserName(HttpContext.User.GetUserName());

        model.User_id = user.Id;

        var result = await _questionService.CreateQuestion(model);

        switch (result)
        {
            case CreateQuestionResult.Success:
                TempData[SuccessMessage] = "سوال شما با موفقیت ثبت شد !";
                return RedirectToAction("Index", "Home", new { area = "UserPanel" });
            case CreateQuestionResult.QuestionNameExist:
                TempData[WarningMessage] = "عنوان سوال شما قبلا به ثبت رسیده است !";
                break;
        }


        return View(model);
    }

    #endregion

    #region edit question 

    [HttpGet("edit-question/{id}")]
    public async Task<IActionResult> EditQuestion(long id)
    {
        var result = await _questionService.FillEditQuestionViewModel(HttpContext.User.GetUserName(), id);

        if (result == null)
        {
            return NotFound();
        }

        return View(result);
    }

    [HttpPost("edit-question/{id}"), ValidateAntiForgeryToken]
    public async Task<IActionResult> EditQuestion(EditQuestionViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var result = await _questionService.EditQuestion(model);

        switch (result)
        {
            case EditQuestionResult.Success:
                TempData[SuccessMessage] = "سوال شما با موفقیت ویرایش شـد !";
                return RedirectToAction("Index", "Home", new { area = "UserPanel" });
            case EditQuestionResult.QuestionNameExist:
                TempData[WarningMessage] = "عنوان سوال شما قبلا ثبت شده است !";
                break;
        }


        return View(model);
    }

    #endregion

    #region answer

    [HttpPost("answer-question")]
    [Authorize]
    public async Task<IActionResult> AnswerQuestion(AnswerQuestionViewModel model)
    {
        if (string.IsNullOrEmpty(model.Message))
        {
            // return new JsonResult(new { status = "EmptyAnswer" });
            TempData[WarningMessage] = "پاسخ نمیتواند خالی باشـد !";
            return RedirectToAction("QuestionDetail", "Home", new { id = model.Question_id });
        }

        var user = await _userService.GetUserByUserName(HttpContext.User.GetUserName());
        model.User_id = user.Id;

        var result = await _questionService.AnswerQuestion(model);
        if (result)
        {
            // return new JsonResult(new { status = "Success" });
            TempData[SuccessMessage] = "پاسخ شما با موفقیت ثبت شد !";
            return RedirectToAction("QuestionDetail", "Home", new { id = model.Question_id });
        }

        // return new JsonResult(new { status = "Error" });
        TempData[ErrorMessage] = "خطایی رخ داده ! لطفا دوباره تلاش کنید";
        return RedirectToAction("Index", "Home");

    }

    [HttpPost("delete-answer/{id}")]
    [Authorize]
    public async Task<IActionResult> DeleteAnswer(long id)
    {

        var user = await _userService.GetUserByUserName(HttpContext.User.GetUserName());
        var answer = await _questionService.GetAnswerById(id);

        var result = await _questionService.CheckDeleteAnswer(user.Username, answer);
        if (result)
        {
            TempData[SuccessMessage] = "پاسخ مورد نظر با موفقیت حذف شد !";
            return RedirectToAction("QuestionDetail", "Home", new { id = answer.Question_id });
        }

        TempData[ErrorMessage] = "خطایی رخ داده ! لطفا دوباره تلاش کنید";
        return RedirectToAction("Index", "Home");

    }

    #region Edit Answer

    [HttpGet("edit-answer/{answerId}")]
    [Authorize]
    public async Task<IActionResult> EditAnswer(long answerId)
    {
        var result = await _questionService.FillEditAnswerViewModel(HttpContext.User.GetUserName(), answerId);
        if (result == null)
        {
            return NotFound();
        }

        return View(result);
    }

    [HttpPost("edit-answer/{answerId}"), ValidateAntiForgeryToken]
    [Authorize]
    public async Task<IActionResult> EditAnswer(EditAnswerViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        model.User_id = User.GetUserId();

        var result = await _questionService.EditAnswer(model);
        var answer = await _questionService.GetAnswerById(model.Answer_id);
        if (result)
        {
            TempData[SuccessMessage] = "عملیات با موفقیت انجام شد !";
            return RedirectToAction("QuestionDetail", "Home", new { id = answer.Question_id });

        }

        TempData[ErrorMessage] = "لطفا دوباره تلاش کنید !";
        return View(model);
    }

    #endregion

    #endregion
}