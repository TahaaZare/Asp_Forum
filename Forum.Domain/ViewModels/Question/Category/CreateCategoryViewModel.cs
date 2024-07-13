using System.ComponentModel.DataAnnotations;

namespace Forum.Domain.ViewModels.Question.Category;

public class CreateCategoryViewModel
{
    [Display(Name = "نام")]
    [Required(ErrorMessage = "{0} را وارد کنید !")]
    public string Name { get; set; }

    [Display(Name = "توضیحات")]
    public string? Description { get; set; }
}

