
using Newtonsoft.Json;

namespace AbuInt.Domain.Commons;

public class Auditable : BaseEntity
{

    [JsonIgnore]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [JsonIgnore]
    public DateTime? UpdatedAt { get; set; }

    [JsonIgnore]
    public int? UpdatedBy { get; set; }

    [JsonIgnore]
    public int? CreatedBy { get; set; }
}