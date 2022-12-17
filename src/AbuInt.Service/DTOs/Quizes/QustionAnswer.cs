using System.ComponentModel.DataAnnotations;

namespace AbuInt.Service.DTOs.Quizes;

public class QustionAnswer
{
    [Required]
    public string Content { get; set; }
    [Required]
    public Question  Question { get; set; }
    [Required]
    public bool IsTrue { get; set; } 
}