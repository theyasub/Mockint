using AbuInt.Domain.Commons;
using System.ComponentModel.DataAnnotations;

namespace AbuInt.Domain.Entities.Quizes;

public class Question : Auditable
{
    [MaxLength(255)]
    public string Content { get; set; }

    public int QuizeId { get; set; }
    public Quize Quize { get; set; }

    public virtual ICollection<QuestionAnswer> Answers { get; set; }
}
