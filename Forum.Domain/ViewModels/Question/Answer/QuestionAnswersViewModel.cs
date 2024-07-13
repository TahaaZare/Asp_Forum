using Forum.Domain.Models.Questions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Domain.ViewModels.Question.Answer
{
    public class QuestionAnswersViewModel
    {
        public Models.Questions.Question Question { get; set; }
        public List<Domain.Models.Questions.Answer> Answers { get; set; }
    }
}
