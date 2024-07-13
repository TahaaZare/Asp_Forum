using Forum.Data.Context;
using Forum.Data.Repositories.Interfaces.Question;
using Forum.Domain.Enums.Admin.Question.Category;
using Forum.Domain.Models.Questions;
using Forum.Domain.ViewModels.Question.Category;
using Microsoft.EntityFrameworkCore;

namespace Forum.Data.Repositories.Implementations.Question;

public class QuestionCategoryRepository : IQuestionCategoryRepository
{
    #region ctor

    private ForumDbContext _context;

    public QuestionCategoryRepository(ForumDbContext context)
    {
        _context = context;
    }

    #endregion


    public async Task<List<QuestionCategory>> GetAllQuestionCategories()
    {
        var categories = await _context.QuestionCategories.ToListAsync();
        return categories;
    }

    public async Task<QuestionCategory> GetQuestionCategoryByName(string categoryName)
    {
        return await _context.QuestionCategories.FirstOrDefaultAsync(s => s.Name == categoryName);
    }

    public async Task<QuestionCategory> GetCategoryById(long categoryId)
    {
        var category = await _context.QuestionCategories.FindAsync(categoryId);
        return category;
    }

    public async Task<CreateCategoryResult> CreateCategory(CreateCategoryViewModel model)
    {
        var checkCategory = await GetQuestionCategoryByName(model.Name.Trim().Replace(" ", "-"));

        if (checkCategory != null)
        {
            return CreateCategoryResult.CategoryNameExist;
        }

        var category = new QuestionCategory()
        {
            Name = model.Name.Trim().Replace(" ", "-"),
            Description = model.Description
        };
        await AddCategory(category);
        await SaveChanges();

        return CreateCategoryResult.Success;
    }

    public async Task AddCategory(QuestionCategory category)
    {
        await _context.AddAsync(category);
    }

    public async Task UpdateCategory(QuestionCategory category)
    {
        _context.QuestionCategories.Update(category);
    }

    public async Task DeleteCategory(QuestionCategory category)
    {
        if (category.Deleted_at == null)
        {
            category.Deleted_at = DateTime.Now;
        }
        else
        {
            category.Deleted_at = null;
        }

        await UpdateCategory(category);
        await SaveChanges();
    }

    public async Task SaveChanges()
    {
        await _context.SaveChangesAsync();
    }
}