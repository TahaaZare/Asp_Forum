using System.ComponentModel.DataAnnotations;

namespace Forum.Domain.ViewModels.Auth;

public class RegisterViewModel
{
    [Required(ErrorMessage = "{0} را وارد کنید !")]
    [Display(Name = "نام کاربری")]
    [MaxLength(150)]
    public string Username { get; set; }

    [Required(ErrorMessage = "{0} را وارد کنید !")]
    [Display(Name = "رمز عبور")]
    [MaxLength(20)]
    public string Password { get; set; }

    [Display(Name = "تکرار کلمه عبور")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    [MaxLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
    [Compare("Password", ErrorMessage = "کلمه های عبور مغایرت دارند .")]
    public string RePassword { get; set; }
}

public enum RegisterResult
{
    Success,
    UserNameExist
}