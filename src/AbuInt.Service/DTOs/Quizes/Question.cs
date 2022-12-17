using System.ComponentModel.DataAnnotations;

namespace AbuInt.Service.DTOs.Quizes;

public class Question
{
    [Required]
    public string Content { get; set; }
    [Required]
    public Quize Quize { get; set; }
    [Required]
    public QustionAnswer Answer { get; set; }
}