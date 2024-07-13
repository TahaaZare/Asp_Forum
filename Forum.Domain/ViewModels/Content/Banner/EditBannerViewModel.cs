using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Forum.Domain.ViewModels.Content.Banner;

public class EditBannerViewModel
{
    public long Id { get; set; }
    [Display(Name = "عنوان")] public string? Title { get; set; }
    [Display(Name = "آدرس")] public string? Url { get; set; }
    public string? ImageFileName { get; set; }
    [Display(Name = "عکس")] public IFormFile? Image { get; set; }
}

public enum EditBannerResult
{
    Success,
    NotFound
}