using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using Forum.Domain.Models.Account;

namespace Forum.Domain.ViewModels.Account.UserPanel;

public class EditUserViewModel
{
    public string Username { get; set; }
    public string? NewUsername { get; set; } 

    [Display(Name = "نام نمایشی")]
    [MaxLength(150)]
    public string? Display_Name { get; set; }

    [Display(Name = "بیوگرافـی")] public string? Bio { get; set; }
}

public enum EditUserResult
{
    Success,
    UserNameExists,
    UserNotFound
}