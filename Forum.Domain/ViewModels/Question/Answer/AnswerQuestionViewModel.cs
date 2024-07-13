using System.ComponentModel.DataAnnotations;

namespace Forum.Domain.ViewModels.Question.Answer;

public class AnswerQuestionViewModel
{
    [Display(Name = "پاسخ")]
    [Required(ErrorMessage = "{0} را وارد نمایید !")]
    public string Message { get; set; }

    public long Question_id { get; set; }
    public long User_id { get; set; }
}