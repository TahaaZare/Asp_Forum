using Forum.Application.Extensions;
using Forum.Application.Services.Interfaces.Account;
using Forum.Application.Services.Interfaces.Question;
using Microsoft.AspNetCore.Mvc;

namespace Forum.Web.Areas.UserPanel.ViewComponents;

public class UserQuestionListViewComponent : ViewComponent
{
    #region ctor

    private IQuestionService _questionService;
    private IUserService _userService;

    public UserQuestionListViewComponent(IQuestionService questionService, IUserService userService)
    {
        _questionService = questionService;
        _userService = userService;
    }

    #endregion

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var user = await _userService.GetUserByUserName(HttpContext.User.GetUserName());
        var question = await _questionService.UserQuestionList(user.Username);

        return View("UserQuestionList",question);
    }
}