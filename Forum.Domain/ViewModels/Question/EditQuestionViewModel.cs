using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Domain.ViewModels.Question
{
    public class EditQuestionViewModel
    {
        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "{0} را وارد کنید !")]
        [MaxLength(150, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string Title { get; set; }

        [Display(Name = "توضیحات")]
        [Required(ErrorMessage = "{0} را وارد کنید !")]
        public string Description { get; set; }

        public List<long> Tag_ids { get; set; }
        public long Category_id { get; set; }
        public long User_id { get; set; }
        public long Question_id { get; set; }
    }
    public enum EditQuestionResult
    {
        Success,
        QuestionNameExist
    }
}

