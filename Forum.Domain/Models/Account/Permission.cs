using System.ComponentModel.DataAnnotations;
using Forum.Domain.Models.Commons;

namespace Forum.Domain.Models.Account;

public class Permission
{
    #region Props

    [Key] public long Id { get; set; }

    [Required(ErrorMessage = "{0} را وارد کنید")]
    [Display(Name = "عنوان")]
    public string Title { get; set; }

    #endregion

    #region relations

    #region user permission

    public ICollection<UserPermission> UserPermissions { get; set; }

    #endregion

    #endregion
}