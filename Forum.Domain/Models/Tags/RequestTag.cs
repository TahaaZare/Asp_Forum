using System.ComponentModel.DataAnnotations.Schema;
using Forum.Domain.Models.Account;
using Forum.Domain.Models.Commons;

namespace Forum.Domain.Models.Tags;

public class RequestTag : BaseModel
{
    #region props

    public string Title { get; set; }

    #endregion

    #region relatiosn

    #region user

    public long User_id { get; set; }
    [ForeignKey("User_id")] public User User { get; set; }

    #endregion

    #endregion
}