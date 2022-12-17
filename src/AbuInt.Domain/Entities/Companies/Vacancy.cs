using AbuInt.Domain.Commons;
using AbuInt.Domain.Entities.Users;
using AbuInt.Domain.Enums;

namespace AbuInt.Domain.Entities.Companies;

public class Vacancy : Auditable
{
    public int  CompanyId { get; set; }
    public Company Company { get; set; }

    public int UserId { get; set; }
    public User User { get; set; }

    public Degree Degree { get; set; }
    public string Description { get; set; }
    public int Price { get; set; }
    public JobLocationType LocationType { get; set; }
    public JobType JobType { get; set; }
}