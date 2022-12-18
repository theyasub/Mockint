using AbuInt.Service.DTOs.Users;
using System.ComponentModel.DataAnnotations;

namespace AbuInt.Service.DTOs.Companies;

public class InterviewForCreationDto
{
    [Required]
    public UserForCreationDto UserForCreationDto { get; set; }
    [Required]
    public UserForCreationDto InterviewerForCreationDto { get; set; }
    [Required]
    public DateTime StartTime { get; set; }
}