using System.ComponentModel.DataAnnotations;
using Forum.Domain.Models.Commons;

namespace Forum.Domain.Models.Questions;

public class QuestionCategory : BaseModel
{
    [Display(Name = "نام")]
    [Required(ErrorMessage = "{0} را وارد کنید !")]
    public string Name { get; set; }

    [Display(Name = "توضیحات")]
    public string? Description { get; set; }
}