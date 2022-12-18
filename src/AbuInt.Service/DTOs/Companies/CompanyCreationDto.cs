using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace AbuInt.Service.DTOs.Companies;

public class CompanyCreationDto
{
    [Required(ErrorMessage = "Company name is required")]
    public string Name { get; set; }

    public string Location { get; set; }

    public IFormFile Image { get; set; }
}