using System.ComponentModel.DataAnnotations.Schema;
using Forum.Domain.Models.Commons;

namespace Forum.Domain.Models.Questions;

public class QuestionVisit : BaseModel
{
    #region props

    public string User_Ip { get; set; }

    #endregion

    #region relations

    #region question

    public long Question_id { get; set; }
    [ForeignKey("Question_id")] public Question Question { get; set; }

    #endregion

    #endregion
}