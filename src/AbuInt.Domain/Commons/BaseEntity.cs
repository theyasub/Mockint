using System.ComponentModel.DataAnnotations;

namespace AbuInt.Domain.Commons;

public class BaseEntity
{
    [Key]
    public int Id { get; set; }
}