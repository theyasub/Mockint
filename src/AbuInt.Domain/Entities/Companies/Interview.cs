using AbuInt.Domain.Commons;
using AbuInt.Domain.Entities.Users;

namespace AbuInt.Domain.Entities.Companies;

public class Interview : Auditable
{
    public int  UserId { get; set; }
    public User User { get; set; }

    public int InterviewerId { get; set; }
    public User Interviewer { get; set; }

    public DateTime StartTime { get; set; }
}