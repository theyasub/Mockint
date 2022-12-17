using System.ComponentModel.DataAnnotations;
using AbuInt.Domain.Enums;
using AbuInt.Service.DTOs.Users;

namespace AbuInt.Service.DTOs.Company;

public class VacancyForCreationDto
{
    [Required]
    public CompanyCreationDto CompanyCreationDto { get; set; }
    
    [Required]
    public UserForCreationDto UserForCreationDto { get; set; }
    
    [Required]
    public Degree Degree { get; set; }
    
    [Required]
    public string Description { get; set; }
    public double price { get; set; }
    
    [Required]
    public JobLocationType JobLocationType { get; set; }
    
    [Required]
    public JobType JobType { get; set; }
    
}