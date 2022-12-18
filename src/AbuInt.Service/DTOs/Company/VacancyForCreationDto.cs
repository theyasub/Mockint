using AbuInt.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace AbuInt.Service.DTOs.Company;

public class VacancyForCreationDto
{
    [Required]
    public int CompanyId { get; set; }

    [Required]
    public int HRId { get; set; }

    [Required]
    public Degree Degree { get; set; }

    [Required]
    public string Description { get; set; }
    public double Price { get; set; }

    [Required]
    public JobLocationType JobLocationType { get; set; }

    [Required]
    public JobType JobType { get; set; }

}