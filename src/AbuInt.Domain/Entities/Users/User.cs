using AbuInt.Domain.Commons;
using AbuInt.Domain.Entities.Chats;
using AbuInt.Domain.Entities.Commons;
using AbuInt.Domain.Entities.Companies;
using AbuInt.Domain.Enums;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace AbuInt.Domain.Entities.Users;

public class User : Auditable
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Username { get; set; }
    public string Gmail { get; set; }

    [JsonIgnore]
    public string Password { get; set; }
    public Role Role { get; set; }

    [JsonIgnore]
    public bool IsEmailVerified { get; set; }

    [JsonIgnore]
    public Guid Salt { get; set; } = Guid.NewGuid();

    public int ImageId { get; set; }
    public Asset Image { get; set; }

    public int? UserDetailId { get; set; }
    [ForeignKey("UserDetailsId")]
    public virtual UserDetail UserDetail { get; set; }

    public virtual ICollection<Participant> Participants { get; set; }
    public virtual ICollection<Message> Messages { get; set; } 
    public virtual ICollection<Vacancy> Vacancies { get; set; } 
    
}