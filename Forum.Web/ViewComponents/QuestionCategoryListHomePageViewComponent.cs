using Forum.Application.Services.Interfaces.Question;
using Microsoft.AspNetCore.Mvc;

namespace Forum.Web.ViewComponents
{
    public class QuestionCategoryListHomePageViewComponent : ViewComponent
    {
        #region ctor

        private IQuestionCategoryService _questionCategoryService;
        public QuestionCategoryListHomePageViewComponent(IQuestionCategoryService  questionCategoryService)
        {
            _questionCategoryService = questionCategoryService;
        }

        #endregion


        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories = await _questionCategoryService.GetAllQuestionCategories();
            return View("QuestionCategoryListHomePage", categories);
        }
    }
}
