using System.ComponentModel.DataAnnotations;
using Azure.Core;
using Forum.Domain.Models.Commons;
using Forum.Domain.Models.Questions;
using Forum.Domain.Models.Tags;

namespace Forum.Domain.Models.Account;

public class User : BaseModel
{
    #region Properties

    [Required(ErrorMessage = "{0} را وارد کنید !")]
    [Display(Name = "نام کاربری")]
    [MaxLength(150, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
    public string Username { get; set; }


    [Display(Name = "نام نمایشی")]
    [MaxLength(150, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
    public string? Display_Name { get; set; }

    [Display(Name = "موبایل")]
    [MaxLength(20)]
    public string? Mobile { get; set; }

    [Required(ErrorMessage = "{0} را وارد کنید !")]
    [Display(Name = "رمز عبور")]
    public string Password { get; set; }

    [Display(Name = "بیوگرافـی")] public string? Bio { get; set; }
    public bool Ban { get; set; }
    public bool Admin { get; set; }
    public string? Avatar { get; set; }

    #endregion

    #region Relations

    #region question

    public ICollection<Question> Questions { get; set; }

    #endregion

    #region question tag

    public ICollection<QuestionTag> QuestionTags { get; set; }

    #endregion

    #region request question tag

    public ICollection<RequestTag> RequestTags { get; set; }

    #endregion

    #region answer

    public ICollection<Answer> Answers { get; set; }

    #endregion

    #region user permission

    public ICollection<UserPermission> UserPermissions { get; set; }

    #endregion

    #endregion
}