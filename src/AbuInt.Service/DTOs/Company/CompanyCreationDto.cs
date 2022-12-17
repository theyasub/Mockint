using System.ComponentModel.DataAnnotations;
using AbuInt.Service.DTOs.Users;

namespace AbuInt.Service.DTOs.Company;

public class CompanyCreationDto
{
    [Required(ErrorMessage = "Company name is required")]
    public string Name { get; set; }
    
    public string Location { get; set; }
    public UserForCreationDto UserForCreationDto { get; set; }
}   