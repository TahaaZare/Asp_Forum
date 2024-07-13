using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Forum.Domain.Models.Account;
using Forum.Domain.Models.Commons;

namespace Forum.Domain.Models.Questions;

public class Answer : BaseModel
{
    #region props

    [Display(Name = "جواب")] public string Description { get; set; }

    public bool IsCorrectAnswer { get; set; }

    #endregion

    #region relations

    #region user

    public long User_id { get; set; }
    [ForeignKey("User_id")] public User User { get; set; }

    #endregion

    #region question

    public long Question_id { get; set; }
    [ForeignKey("Question_id")] public Question Question { get; set; }

    #endregion

    #endregion
}