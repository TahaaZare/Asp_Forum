using System.ComponentModel.DataAnnotations;

namespace Forum.Domain.ViewModels.Content.Banner;

public class BannerListViewModel
{
    [Display(Name = "عنوان")] public string? Title { get; set; }
    [Display(Name = "آدرس")] public string? Url { get; set; }
    [Display(Name = "عکس")] public string Image { get; set; }
}