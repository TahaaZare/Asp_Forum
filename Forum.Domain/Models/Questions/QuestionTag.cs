using System.ComponentModel.DataAnnotations.Schema;
using Forum.Domain.Models.Commons;
using Forum.Domain.Models.Tags;

namespace Forum.Domain.Models.Questions;

public class QuestionTag : BaseModel
{
    #region relations

    #region question

    public long Question_id { get; set; }
    [ForeignKey("Question_id")] public Question Question { get; set; }

    #endregion

    #region tag

    public long Tag_id { get; set; }
    [ForeignKey("Tag_id")] public Tag Tag { get; set; }

    #endregion

    #endregion
}