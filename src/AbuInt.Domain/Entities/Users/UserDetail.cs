using AbuInt.Domain.Commons;
using AbuInt.Domain.Entities.Commons;
using AbuInt.Domain.Enums;

namespace AbuInt.Domain.Entities.Users;

public class UserDetail : Auditable
{
    public virtual ICollection<Experience> ExperiencesId { get; set; }
    public string Phonenumber { get; set; }
    public Degree Degree { get; set; }

    public int ResumeId { get; set; }
    public Asset Resume { get; set; }

    public User User { get; set; }
}