using AbuInt.Domain.Commons;

namespace AbuInt.Domain.Entities.Quizes;

public class QuestionAnswer : Auditable
{
    public string Content { get; set; }
    public bool IsTrue { get; set; }

    public int QuestionId { get; set; }
    public Question Question { get; set; }
}
