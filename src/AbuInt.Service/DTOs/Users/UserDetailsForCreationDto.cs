using AbuInt.Domain.Enums;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace AbuInt.Service.DTOs.Users;

public class UserDetailsForCreationDto
{
    public ExperienceForCreationDto ExpeianceForCreationDto { get; set; }

    [Required(ErrorMessage = "Phonenumber is required")]
    public string PhoneNumber { get; set; }

    [Required(ErrorMessage = "Deggre is required")]
    public Degree Degree { get; set; } = Degree.Junior;


    [DataType(DataType.Upload)]
    [System.ComponentModel.Bindable(true)]
    public IFormFile Resume { get; set; } = null!;
}