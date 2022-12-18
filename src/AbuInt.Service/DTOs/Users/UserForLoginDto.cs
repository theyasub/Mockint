using System.ComponentModel.DataAnnotations;

namespace AbuInt.Service.DTOs.Users;

public class UserForLoginDto
{
    [Required(ErrorMessage = "Gmail is required")]
    [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",
        ErrorMessage = "Please enter valid gmail")]
    public string Gmail { get; set; } = string.Empty;


    [Required(ErrorMessage = "Password is required"),DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;
}