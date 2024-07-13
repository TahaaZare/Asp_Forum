using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Forum.Domain.Models.Account;
using Forum.Domain.Models.Commons;

namespace Forum.Domain.Models.Questions;

public class Question : BaseModel
{
    #region props

    [Required(ErrorMessage = "{0} را وارد کنید !")]
    [MaxLength(150, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
    public string Title { get; set; }

    [Required(ErrorMessage = "{0} را وارد کنید !")]
    public string Description { get; set; }

    public bool Status { get; set; } = true;
    public int Visit { get; set; } = 0;


    #endregion

    #region Relations

    #region User

    public long User_id { get; set; }
    [ForeignKey("User_id")] public User User { get; set; }

    #endregion

    #region Question Category

    public long Category_id { get; set; }
    [ForeignKey("Category_id")] public QuestionCategory QuestionCategory { get; set; }

    #endregion

    #region visit

    public ICollection<QuestionVisit> QuestionVisits { get; set; }

    #endregion

    #endregion
}