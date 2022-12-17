using System.ComponentModel.DataAnnotations;
using AbuInt.Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace AbuInt.Service.DTOs.Users;

public class UserForCreationDto
{
    [Required(ErrorMessage = "FirstName is required")]
    public string FirstName { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Lastname is required")]
    public string LastName { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Username is required")]
    public string Username { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Please enter valid email")]
    public string Email { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Password is required"),DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;

    [Required(ErrorMessage = "Choose Role")]
    public Role Role { get; set; } = Role.User;
    
    [Required(ErrorMessage = "Image is required")]
    [DataType(DataType.Upload)]
    [System.ComponentModel.Bindable(true)]
    public IFormFile Image { get; set; } = null!;
    
    public UserDetailsForCreationDto? UserDetailsForCreationDto { get; set; }
}