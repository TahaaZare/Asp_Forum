using System.ComponentModel.DataAnnotations;

namespace Forum.Domain.ViewModels.Auth;

public class LoginViewModel
{
    [Required(ErrorMessage = "{0} را وارد کنید !")]
    [Display(Name = "نام کاربری")]
    [MaxLength(150)]
    public string Username { get; set; }

    [Required(ErrorMessage = "{0} را وارد کنید !")]
    [Display(Name = "رمز عبور")]
    [MaxLength(20)]
    public string Password { get; set; }

    public string? ReturnUrl { get; set; }
}

public enum LoginResult
{
    Success,
    Ban,
    NotFound
}