using System.ComponentModel.DataAnnotations;
using AbuInt.Domain.Enums;
using Microsoft.AspNet.Http;

namespace AbuInt.Service.DTOs.Users;

public class UserForCreationDto
{
    [Required(ErrorMessage = "FirstName is required")]
    public string FirstName { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Lastname is required")]
    public string LastName { get; set; } = string.Empty;
    
    [Required]
    public string Username { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Please enter valid email")]
    public string Email { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; } = string.Empty;

    [Required(ErrorMessage = "Choose Role")]
    public Role Role { get; set; } = Role.User;
    
    [Required(ErrorMessage = "Image is required")]
    [DataType(DataType.Upload)]
    [System.ComponentModel.Bindable(true)]
    public IFormFile Image { get; set; } = null!;
    
    [Required(ErrorMessage = "Image is required")]
    [DataType(DataType.Upload)]
    [System.ComponentModel.Bindable(true)]
    public IFormFile Resume { get; set; } = null!;

    
}