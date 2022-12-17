using AbuInt.Domain.Commons;

namespace AbuInt.Domain.Entities.Commons;

public class Attachment : Auditable
{
    public string Name { get; set; }
    public string Path { get; set; }
}
