using System.ComponentModel.DataAnnotations;

namespace AbuInt.Service.DTOs.Users;

public class UserForLoginDto
{
    [Required(ErrorMessage = "Gmail is required")]
    [DataType(DataType.EmailAddress, ErrorMessage = "Please enter valid gmail")]
    public string Gmail { get; set; } = string.Empty;


    [Required(ErrorMessage = "Password is required"),DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;
}