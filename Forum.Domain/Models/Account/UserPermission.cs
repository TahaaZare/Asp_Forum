using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Forum.Domain.Models.Account;

public class UserPermission
{
    #region props

    [Key] public long Id { get; set; }

    #endregion

    #region relations

    #region User

    public long User_Id { get; set; }
    [ForeignKey("User_Id")] public User User { get; set; }

    #endregion

    #region Permmssion

    public long Permission_id { get; set; }
    [ForeignKey("Permission_id")] public Permission Permission { get; set; }

    #endregion

    #endregion
}