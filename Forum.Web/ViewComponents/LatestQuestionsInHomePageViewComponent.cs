using Forum.Application.Services.Interfaces.Question;
using Microsoft.AspNetCore.Mvc;

namespace Forum.Web.ViewComponents
{
    public class LatestQuestionsInHomePageViewComponent : ViewComponent
    {
        #region ctor

        private IQuestionService _questionService;
        public LatestQuestionsInHomePageViewComponent(IQuestionService questionService)
        {
            
            _questionService = questionService;
        }

        #endregion

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var questions = await _questionService.GetFourLatesQuestion();
            return View("LatestQuestionsInHomePage", questions);

        }
    }
}
