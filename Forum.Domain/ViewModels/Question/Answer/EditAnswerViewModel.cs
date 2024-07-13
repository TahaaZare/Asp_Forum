using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Domain.ViewModels.Question.Answer
{
    public class EditAnswerViewModel
    {
        [Display(Name = "پاسخ")]
        [Required(ErrorMessage = "{0} را وارد نمایید !")]
        public string Message { get; set; }

        public long Answer_id { get; set; }
        public long Question_id { get; set; }
        public long User_id { get; set; }
    }
}
