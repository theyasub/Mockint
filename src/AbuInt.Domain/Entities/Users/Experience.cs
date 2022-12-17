using AbuInt.Domain.Commons;

namespace AbuInt.Domain.Entities.Users;

public class Experience : Auditable
{
    public string CompanyName { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public string Description { get; set; }
}