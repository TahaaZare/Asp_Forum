using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Forum.Domain.ViewModels.Content.Banner;

public class CreateBannerViewModel
{
    [Display(Name = "عنوان")] public string? Title { get; set; }
    [Display(Name = "آدرس")] public string? Url { get; set; }

    [Display(Name = "عکس")]
    [Required(ErrorMessage = "{0} را وارد نمایید !")]
    public IFormFile Image { get; set; }
}

public enum CreateBannerResult
{
    Success , ImageUploadFailed
}