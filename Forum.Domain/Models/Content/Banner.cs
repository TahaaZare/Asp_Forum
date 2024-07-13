using System.ComponentModel.DataAnnotations;
using Forum.Domain.Models.Commons;

namespace Forum.Domain.Models.Content;

public class Banner : BaseModel
{
    [Display(Name = "عنوان")] public string? Title { get; set; }
    [Display(Name = "آدرس")] public string? Url { get; set; }

    [Display(Name = "عکس")]
    [Required(ErrorMessage = "{0} را وارد کنید ")]
    public string Image { get; set; }
}