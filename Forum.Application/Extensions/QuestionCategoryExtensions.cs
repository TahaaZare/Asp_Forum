using Forum.Domain.Models.Questions;

namespace Forum.Application.Extensions;

public static class QuestionCategoryExtensions
{
    public static string DisplayName(this QuestionCategory category)
    {
        if (!string.IsNullOrEmpty(category.Description))
        {
            return category.Description;
        }

        return "توضیحاتی وارد نشــده !";
    }
}