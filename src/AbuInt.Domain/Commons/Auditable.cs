namespace AbuInt.Domain.Commons;

public class Auditable : BaseEntities
{
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public int? UpdatedBy { get; set; }
    public int? CreatedBy { get; set; }
}