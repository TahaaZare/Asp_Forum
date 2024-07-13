using Forum.Application.Services.Interfaces.Question;
using Forum.Data.Repositories.Interfaces.Question;
using Forum.Domain.Enums.Admin.Question.Category;
using Forum.Domain.Models.Questions;
using Forum.Domain.ViewModels.Question.Category;

namespace Forum.Application.Services.Implementations.Question;

public class QuestionCategoryService : IQuestionCategoryService
{
    #region ctor

    private IQuestionCategoryRepository _questionCategoryRepository;

    public QuestionCategoryService(IQuestionCategoryRepository questionCategoryRepository)
    {
        _questionCategoryRepository = questionCategoryRepository;
    }

    #endregion

    public async Task<List<QuestionCategory>> GetAllQuestionCategories()
    {
        return await _questionCategoryRepository.GetAllQuestionCategories();
    }

    public async Task<QuestionCategory> GetQuestionCategoryByName(string categoryName)
    {
        return await _questionCategoryRepository.GetQuestionCategoryByName(categoryName);
    }

    public async Task<CreateCategoryResult> CreateCategory(CreateCategoryViewModel model)
    {
        return await _questionCategoryRepository.CreateCategory(model);
    }

    public async Task<EditCategoryViewModel> FillEditCategoryViewModel(string categoryName)
    {
        var category = await _questionCategoryRepository.GetQuestionCategoryByName(categoryName);

        return new EditCategoryViewModel()
        {
            Id = category.Id,
            Name = category.Name,
            Description = category.Description
        };
    }

    public async Task<EditCategoryQuestionResult> EditCategory(EditCategoryViewModel model)
    {
        var category = await _questionCategoryRepository.GetQuestionCategoryByName(model.Name.Trim().Replace(" ", "-"));
        if (model.Id == category.Id)
        {
            if (model.Name != category.Name)
            {
                category.Name = model.Name.Trim().Replace(" ", "-");
                category.Description = model.Description;
                await _questionCategoryRepository.UpdateCategory(category);
                await _questionCategoryRepository.SaveChanges();

                return EditCategoryQuestionResult.Success;
            }
            else
            {
                category.Description = model.Description;
                await _questionCategoryRepository.UpdateCategory(category);
                await _questionCategoryRepository.SaveChanges();

                return EditCategoryQuestionResult.Success;
            }
        }
        else
        {
            return EditCategoryQuestionResult.CategoryNameExist;
        }
    }

    public async Task DeleteCategory(QuestionCategory category)
    {
        await _questionCategoryRepository.DeleteCategory(category);
    }
}