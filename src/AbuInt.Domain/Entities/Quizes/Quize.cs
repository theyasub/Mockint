using AbuInt.Domain.Commons;

namespace AbuInt.Domain.Entities.Quizes;

public class Quize : Auditable
{
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public virtual ICollection<Question> Questions { get; set; }
}
