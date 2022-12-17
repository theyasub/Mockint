using AbuInt.Domain.Commons;
using AbuInt.Domain.Entities.Users;

namespace AbuInt.Domain.Entities.Companies;

public class Company : Auditable
{
    public string Name { get; set; }
    public string Location { get; set; }
    public DateTime Created_at { get; set; }

    public int UserId { get; set; }
    public User User { get; set; }
}