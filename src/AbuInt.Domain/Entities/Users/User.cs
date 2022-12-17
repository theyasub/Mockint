using AbuInt.Domain.Commons;
using AbuInt.Domain.Entities.Commons;
using AbuInt.Domain.Enums;

namespace AbuInt.Domain.Entities.Users;

public class User : Auditable
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string UserName { get; set; }
    public string Gmail { get; set; }
    public string Password { get; set; }
    public Role Role { get; set; }
    public bool IsEmailVerified { get; set; }
    public Guid Salt { get; set; }

    public int UserDetailsId { get; set; }
    public UserDetail UserDetails { get; set; }

    public int ImageId { get; set; }
    public Asset Image { get; set; }
}