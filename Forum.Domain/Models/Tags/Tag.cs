using Forum.Domain.Models.Commons;
using Forum.Domain.Models.Questions;

namespace Forum.Domain.Models.Tags;

public class Tag : BaseModel
{
    public string Title { get; set; }
    
    #region question tag

    public ICollection<QuestionTag> QuestionTags { get; set; }

    #endregion
}