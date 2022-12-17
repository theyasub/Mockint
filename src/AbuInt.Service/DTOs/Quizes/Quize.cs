using System.ComponentModel.DataAnnotations;

namespace AbuInt.Service.DTOs.Quizes;

public class Quize
{
    [Required]
    public int Count { get; set; }
    [Required]
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
}