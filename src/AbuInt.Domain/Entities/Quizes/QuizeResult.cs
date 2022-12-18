using AbuInt.Domain.Commons;
using AbuInt.Domain.Entities.Users;

namespace AbuInt.Domain.Entities.Quizes;

public class QuizeResult : BaseEntity
{
    public int UserId { get; set; }
    public User User { get; set; }

    public int QuizeId { get; set; }
    public Quize Quize { get; set; }

    public double Percentage { get; set; }
}
