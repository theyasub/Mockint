using System.ComponentModel.DataAnnotations;

namespace AbuInt.Service.DTOs.Users;

public class EmailMessage
{
    [Required]
    public string To { get; set; } = string.Empty;

    [Required]
    public string Subject { get; set; } = string.Empty;

    [Required]
    public int Body { get; set; }

    public bool IsEmailConfirmed { get; set; }
}