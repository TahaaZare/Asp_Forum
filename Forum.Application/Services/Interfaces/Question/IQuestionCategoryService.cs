using Forum.Domain.Enums.Admin.Question.Category;
using Forum.Domain.Models.Questions;
using Forum.Domain.ViewModels.Question.Category;

namespace Forum.Application.Services.Interfaces.Question;

public interface IQuestionCategoryService
{
    Task<List<QuestionCategory>> GetAllQuestionCategories();
    Task<QuestionCategory> GetQuestionCategoryByName(string categoryName);
    Task<CreateCategoryResult> CreateCategory(CreateCategoryViewModel model);
    Task<EditCategoryViewModel> FillEditCategoryViewModel(string categoryName);
    Task<EditCategoryQuestionResult> EditCategory(EditCategoryViewModel model);
    Task DeleteCategory(QuestionCategory category);
}