using System.ComponentModel.DataAnnotations;
using AbuInt.Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace AbuInt.Service.DTOs.Users;

public class UserDetailsForCreationDto
{
    public ExpeianceForCreationDto ExpeianceForCreationDto { get; set; }
    
    [Required(ErrorMessage = "Phonenumber is required")]
    public string PhoneNumber { get; set; }

    [Required(ErrorMessage = "Deggre is required")]
    public Degree Degree { get; set; } = Degree.Junior;
    
    [Required(ErrorMessage = "Image is required")]
    [DataType(DataType.Upload)]
    [System.ComponentModel.Bindable(true)]
    public IFormFile Resume { get; set; } = null!;
}