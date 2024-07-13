using Forum.Domain.Enums.Admin.Question.Category;
using Forum.Domain.Models.Questions;
using Forum.Domain.ViewModels.Question.Category;

namespace Forum.Data.Repositories.Interfaces.Question;

public interface IQuestionCategoryRepository
{
    Task<List<QuestionCategory>> GetAllQuestionCategories();
    Task<QuestionCategory> GetQuestionCategoryByName(string categoryName);
    Task<QuestionCategory> GetCategoryById(long categoryId);
    Task<CreateCategoryResult> CreateCategory(CreateCategoryViewModel model);
    Task AddCategory(QuestionCategory category);
    Task UpdateCategory(QuestionCategory category);
    Task DeleteCategory(QuestionCategory category);
    Task SaveChanges();

}